﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;

namespace WebMovie.Backend.BL.TrailerBL
{
    public interface ITrailerBL : IBasaBL<Trailer>
    {
        IEnumerable<Trailer> GetAllTrailerByMovieId(Guid movieId);
    }
}
