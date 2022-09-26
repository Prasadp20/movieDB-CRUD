namespace CRUDonMovieDB.Model
{
    public class Movie : BaseModel
    {
        /*
         * Id,mov_title,mov_year,mov_time,mov_lang,mov_dt_rel,mov_rel_country,act_id,dir_id,gen_id,rev_id
         */

        public int Id { get; set; }
        public  string? mov_title { get; set; }
        public int mov_year { get; set; }
        public int mov_time { get; set; }
        public  string? mov_lang { get; set; }
        public DateTime mov_dt_rel { get; set; }
        public  string? mov_rel_country { get; set; }
        public  string? ActorName { get; set; }
        public  string? DirectorName { get; set; }
        //public int act_id { get; set; }
        //public int dir_id { get; set; }
        //public int gen_id { get; set; }
        //public int rev_id { get; set; }

        
    }

    public class CreateMovieDto
    {
        public int Id { get; set; }
        public string? mov_title { get; set; }
        public int mov_year { get; set; }
        public int mov_time { get; set; }
        public string? mov_lang { get; set; }
        public DateTime mov_dt_rel { get; set; }
        public string? mov_rel_country { get; set; }
        public List<MovieActor> actor { get; set; } = new List<MovieActor>();
        public List<MovieDirector> direct { get; set; } = new List<MovieDirector>();
        public List<MovieGeners> geners { get; set; } = new List<MovieGeners>();
        public List<MovieRatingReviewer> rate { get; set; } = new List<MovieRatingReviewer>();
    }
    public class UpdateMovieDto : CreateMovieDto
    {
        public int act_id { get; set; }
        public int dir_id { get; set; }
        public int gen_id { get; set; }
        public int rev_id { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public int isDeleted { get; set; }


    }
}
