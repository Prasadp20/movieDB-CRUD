namespace CRUDonMovieDB.Model
{
    public class BaseModel
    {
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public int isDeleted { get; set; }


        public string createdDateFormatDate
        {
            get
            {
                DateTime tmp;
                DateTime.TryParse(createdDate.ToString(), out tmp);
                return tmp.ToString("yyyy-MM-ss hh:mm:ss tt");
            }
        }
        public string modifiedDateFormatDate
        {
            get
            {
                DateTime tmp;
                DateTime.TryParse(modifiedDate.ToString(), out tmp);
                return tmp.ToString("yyyy-MM-dd hh:mm:ss tt");
            }
        }

        public class DeleteObj
        {
            public int Id { get; set; }
            public int modifiedBy { get; set; }
            public DateTime modifiedDate { get; set; }
        }
    }
}
