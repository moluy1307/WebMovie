using Dapper;
using Misa.Amis.DL.BaseDL;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Constants;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.UserDL;

namespace WebMovie.Backend.DL.MovieDL
{
    public class MovieDL : BaseDL<Movie>, IMovieDL
    {
        public IEnumerable<Movie> GetAllMovies()
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM Movie";

                var numberOfAffectedRows = mySqlConnection.Query<Movie>(query);

                return numberOfAffectedRows;
            }
        }

        public Movie GetMovieById(Guid movieId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Movie).Name, "ById");
            string storedProceduredNameCategory = String.Format(ProcedureName.Get, typeof(MovieCategory).Name, "ByMovieId");
            string storedProceduredNameActor = String.Format(ProcedureName.Get, typeof(MovieActor).Name, "ByMovieId");
            string storedProceduredNameEpisode = String.Format(ProcedureName.Get, typeof(Episode).Name, "AllByMovieId");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_{typeof(Movie).Name}Id", movieId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.QueryFirstOrDefault<Movie>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //if(recordById != null && !string.IsNullOrEmpty(recordById.ImagePath))
                //{
                //    recordById.ImagePath = Convert.ToBase64String(File.ReadAllBytes(recordById.ImagePath));
                //}
                if (recordById != null)
                {
                    var listEpisode = mySqlConnection.Query<Episode>(storedProceduredNameEpisode, parameters, commandType: CommandType.StoredProcedure);
                    var listCategory = mySqlConnection.Query<MovieCategory>(storedProceduredNameCategory, parameters, commandType: CommandType.StoredProcedure);
                    var listActor = mySqlConnection.Query<MovieActor>(storedProceduredNameActor, parameters, commandType: CommandType.StoredProcedure);

                    recordById.Categories = listCategory.ToList();
                    recordById.Episodes = listEpisode.ToList();
                    recordById.Actors = listActor.ToList();
                }
                return recordById;
            }
        }

        public int InsertMovie(Movie movie)
        {
            string storedProceduredName = "Proc_Movie_InsertMultiple";

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            //Lấy tất cả các thuộc tính có trong object record (Employee, Department,...)
            var properties = typeof(Movie).GetProperties();
            var propertiesEpisode = typeof(Episode).GetProperties();

            Guid movieSingleId = Guid.NewGuid();

            //movie.Categories = JsonSerializer.Deserialize<List<MovieCategory>>(model.DetailsString);
            //string detailsJson = JsonConvert.SerializeObject(movie.Categories);
            //var details = JsonConvert.DeserializeObject<List<MovieCategory>>(detailsJson);
            //var details = JsonConvert.DeserializeObject<List<MovieCategory>>(movie.CateMovie);
            //movie.Categories = details;

            //Duyệt qua tất cả các thuộc tính có trong object record (Employee, Department,...)
            foreach (var property in properties)
            {
                //Đọc tên thuộc tính có trong object record (Employee, Department,...)
                string name = property.Name;

                //Lấy giá trị của các thuộc tính
                var value = property.GetValue(movie);

                //Kiểm tra thuộc tính có phải là khóa chính không
                var primaryKey = Attribute.IsDefined(property, typeof(KeyAttribute));

                //Sinh dữ liệu cho khóa chính
                if (primaryKey == true)
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        value = movieSingleId;
                    }
                }

                //Kiểm tra thuộc tính có không có đối số
                var noParam = Attribute.IsDefined(property, typeof(NoParam));

                if (noParam == true)
                {
                    continue;
                }

                //Viết đối số trong DB
                string argument = "@p_" + name;

                parameters.Add($"{argument}", value);
            }

            parameters.Add("p_CreatedDate", DateTime.Now);

            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        //string query = "INSERT INTO Movie (MovieId, MovieCode, MovieName, ImagePath) VALUES (@MovieId, @MovieCode, @MovieName, @ImagePath)";

                        //numberOfAffectedRows = mySqlConnection.Execute(query, param: new { @MovieId = Guid.NewGuid(), @MovieCode = movie.MovieCode, @MovieName = movie.MovieName, @ImagePath  = movie.ImagePath});
                        numberOfAffectedRows = mySqlConnection.Execute(storedProceduredName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        if (numberOfAffectedRows > 0)
                        {
                            //if(movie.Categories != null && movie.Categories.Count > 0)
                            //{
                            foreach (var cat in movie.Categories)
                            {
                                mySqlConnection.Execute("INSERT INTO moviecategory (MovieID, Value) VALUES (@MovieID, @Value);", new { MovieID = movieSingleId, Value = cat.Value }, transaction: transaction);
                            }
                            //}

                            //Thêm diễn viên cho phim
                            foreach (var act in movie.Actors)
                            {
                                mySqlConnection.Execute("INSERT INTO movieactor (MovieID, Value) VALUES (@MovieID, @Value);", new { MovieID = movieSingleId, Value = act.Value }, transaction: transaction);
                            }

                            //Thêm mới bộ phim
                            foreach (var ep in movie.Episodes)
                            {
                                mySqlConnection.Execute(@"INSERT INTO episode (EpisodeId, EpisodeName,EpisodeNumber,EpisodeUrl,MovieId,CreatedDate) 
                                                            VALUES (@p_EpisodeId, @p_EpisodeName, @p_EpisodeNumber, @p_EpisodeUrl, @p_MovieId, @p_CreatedDate);",
                                    new
                                    {
                                        p_EpisodeId = Guid.NewGuid(),
                                        p_EpisodeName = ep.EpisodeName,
                                        p_EpisodeNumber = ep.EpisodeNumber,
                                        p_EpisodeUrl = ep.EpisodeUrl,
                                        p_MovieId = movieSingleId,
                                        p_CreatedDate = DateTime.Now,
                                    },
                                    transaction: transaction);
                            }

                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }


            }

            return numberOfAffectedRows;
        }

        //public IEnumerable<Movie> GetAllMovieByType(int typeMovie, int? limit)
        //{
        //    //Chuẩn bị tên stored procedure
        //    string storedProceduredName = String.Format(ProcedureName.Get, typeof(Movie).Name, "ByType");
        //    string storedProceduredNameEpisode = String.Format(ProcedureName.Get, typeof(Episode).Name, "ByNewest");

        //    //Chuẩn bị tham số đầu vào cho stored
        //    var parameters = new DynamicParameters();
        //    parameters.Add("p_TypeMovie", typeMovie);
        //    parameters.Add("p_Limit", limit);

        //    //Khởi tạo kết nối đến DB
        //    using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
        //    {
        //        mySqlConnection.Open();

        //        //Gọi vào DB
        //        var numberOfAffectedRows = mySqlConnection.Query<Movie>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

        //        if (numberOfAffectedRows != null)
        //        {
        //            foreach (var movieItem in numberOfAffectedRows)
        //            {
        //                var newEstEpisode = mySqlConnection.QueryFirstOrDefault<Episode>(storedProceduredNameEpisode, new { p_MovieId = movieItem.MovieId }, commandType: CommandType.StoredProcedure);
        //                movieItem.NewestEpisode = newEstEpisode;
        //            }
        //        }
        //        return numberOfAffectedRows;
        //    }
        //}

        public PagingResult<Movie> GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Movie).Name, "ByType");
            string storedProceduredNameEpisode = String.Format(ProcedureName.Get, typeof(Episode).Name, "ByNewest");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("@p_PageNumber", pageNumber);
            parameters.Add("@p_PageSize", pageSize);
            parameters.Add("p_CategoryId", categoryId);
            parameters.Add("p_TypeMovie", typeMovie);
            parameters.Add("p_ColumnFilter", columnFilter);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                //var numberOfAffectedRows = mySqlConnection.Query<Movie>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //Gọi vào DB
                var reader = mySqlConnection.QueryMultiple(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //var multi = mySqlConnection.QueryMultiple(storedProceduredTotalRecords, commandType: CommandType.StoredProcedure);
                int count = reader.Read<int>().FirstOrDefault();
                var listRecordByFilterPaging = reader.Read<Movie>().ToList();
                var numberOfAffectedRows = new PagingResult<Movie>(count, pageNumber, pageSize, listRecordByFilterPaging);

                if (numberOfAffectedRows != null)
                {
                    foreach (var movieItem in numberOfAffectedRows.Data)
                    {
                        var newEstEpisode = mySqlConnection.QueryFirstOrDefault<Episode>(storedProceduredNameEpisode, new { p_MovieId = movieItem.MovieId }, commandType: CommandType.StoredProcedure);
                        movieItem.NewestEpisode = newEstEpisode;
                    }
                }
                return numberOfAffectedRows;
            }
        }

        public PagingResult<Movie> GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Movie).Name, "SimilarMovie");
            string storedProceduredNameEpisode = String.Format(ProcedureName.Get, typeof(Episode).Name, "ByNewest");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("@p_PageNumber", pageNumber);
            parameters.Add("@p_PageSize", pageSize);
            parameters.Add("p_MovieId", movieId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                //var numberOfAffectedRows = mySqlConnection.Query<Movie>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //Gọi vào DB
                var reader = mySqlConnection.QueryMultiple(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //var multi = mySqlConnection.QueryMultiple(storedProceduredTotalRecords, commandType: CommandType.StoredProcedure);
                int count = reader.Read<int>().FirstOrDefault();
                var listRecordByFilterPaging = reader.Read<Movie>().ToList();
                var numberOfAffectedRows = new PagingResult<Movie>(count, pageNumber, pageSize, listRecordByFilterPaging);

                if (numberOfAffectedRows != null)
                {
                    foreach (var movieItem in numberOfAffectedRows.Data)
                    {
                        var newEstEpisode = mySqlConnection.QueryFirstOrDefault<Episode>(storedProceduredNameEpisode, new { p_MovieId = movieItem.MovieId }, commandType: CommandType.StoredProcedure);
                        movieItem.NewestEpisode = newEstEpisode;
                    }
                }
                return numberOfAffectedRows;
            }
        }

        public int UpdateMovie(Guid movieId, Movie movie)
        {
            var res = UpdateRecord(movieId, movie);

            if (res > 0)
            {
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    mySqlConnection.Open();
                    using (var transaction = mySqlConnection.BeginTransaction())
                    {
                        try
                        {
                            //Cập nhật thể loại
                            int deleteCategory = mySqlConnection.Execute("DELETE FROM moviecategory WHERE MovieId=@p_MovieId;", new { p_MovieId = movieId }, transaction: transaction);

                            if (deleteCategory >= 0)
                            {
                                foreach (var cat in movie.Categories)
                                {
                                    mySqlConnection.Execute("INSERT INTO moviecategory (MovieID, Value) VALUES (@MovieID, @Value);", new { MovieID = movieId, Value = cat.Value }, transaction: transaction);
                                }
                            }

                            //Cập nhật diễn viên cho phim
                            int deleteActor = mySqlConnection.Execute("DELETE FROM movieactor WHERE MovieId=@p_MovieId;", new { p_MovieId = movieId }, transaction: transaction);

                            if (deleteActor >= 0)
                            {
                                foreach (var act in movie.Actors)
                                {
                                    mySqlConnection.Execute("INSERT INTO movieactor (MovieID, Value) VALUES (@MovieID, @Value);", new { MovieID = movieId, Value = act.Value }, transaction: transaction);
                                }
                            }

                            //Cập nhật bộ phim
                            int deleteEpisode = mySqlConnection.Execute("DELETE FROM episode WHERE MovieId=@p_MovieId;", new { p_MovieId = movieId }, transaction: transaction);

                            if (deleteEpisode >= 0)
                            {
                                foreach (var ep in movie.Episodes)
                                {
                                    mySqlConnection.Execute(@"INSERT INTO episode (EpisodeId, EpisodeName,EpisodeNumber,EpisodeUrl,MovieId,CreatedDate) 
                                                            VALUES (@p_EpisodeId, @p_EpisodeName, @p_EpisodeNumber, @p_EpisodeUrl, @p_MovieId, @p_CreatedDate);",
                                        new
                                        {
                                            p_EpisodeId = Guid.NewGuid(),
                                            p_EpisodeName = ep.EpisodeName,
                                            p_EpisodeNumber = ep.EpisodeNumber,
                                            p_EpisodeUrl = ep.EpisodeUrl,
                                            p_MovieId = movieId,
                                            p_CreatedDate = DateTime.Now,
                                        },
                                        transaction: transaction);
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine(ex.Message);
                        }
                    }

                }
            }

            return res;
        }

        public int UpdateReview(Guid movieId)
        {
            var movieById = GetRecordById(movieId);
            int totalReview = 0;
            if(movieById.MovieReview == null)
            {
                totalReview = 1;
            }
            else
            {
                totalReview = (int)movieById.MovieReview + (int)1;
            }

            int numberOfAffectedRows = 0;
            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mySqlConnection.Execute("UPDATE Movie SET MovieReview = @p_MovieReview WHERE MovieId = @p_MovieId;",
                             new { p_MovieReview = totalReview, p_MovieId = movieId }, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return numberOfAffectedRows;
        }

        public int UpdateMediumScore(Guid movieId, decimal mediumScore)
        {
            int numberOfAffectedRows = 0;
            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mySqlConnection.Execute("UPDATE Movie SET MediumScore = @p_MediumScore WHERE MovieId = @p_MovieId;",
                             new { p_MediumScore = mediumScore, p_MovieId = movieId }, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return numberOfAffectedRows;
        }
    }
}
