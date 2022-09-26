namespace CRUDonMovieDB.Model
{
    public class MovieRatingReviewer
    {
        /* rev_id
           rev_stars
           num_o_ratings
           rev_name   */

        public int rev_id { get; set; }
        public  float rev_stars { get; set; }
        public int num_o_ratings { get; set; }
        public string? rev_name { get; set; }
    }
}