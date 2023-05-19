namespace WebMovie.Backend.Common
{
    /// <summary>
    /// Attribute đánh dấu thuộc tính là bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckIsEmpty : Attribute
    {
    }

    /// <summary>
    /// Attribute hiển thị đầy đủ tên thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name) 
        {
            Name = name;
        }
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính là mã của object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RecordCode : Attribute
    {
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính được xuất ra file excel
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportExcel : Attribute
    {
    }

    /// <summary>
    /// Attribute hiển thị đầy đủ tên đối tượng
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectName : Attribute
    {
        public string Name = string.Empty;
        public ObjectName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Attribute đánh dấu đối tượng được lưu dưới cấu trúc dạng cây
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TreeStructure : Attribute
    {
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính là khóa ngoại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
    }

    /// <summary>
    /// Attribute set độ dài tối thiểu của thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLengthCustom : Attribute
    {
        private readonly int ValueMinLength;

        public MinLengthCustom(int valueMinLength)
        {
            ValueMinLength = valueMinLength;
        }

        public int ValueMin
        {
            get { return ValueMinLength; }
        }
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính là id gốc
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsRootId : Attribute
    {
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính là trạng thái
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class isStatus : Attribute
    {
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính không có đối số để truy cập vào CSDL
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NoParam : Attribute
    {
    }

    /// <summary>
    /// Attribute đánh dấu thuộc tính là mật khẩu
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class isPassword : Attribute
    {
    }

}
