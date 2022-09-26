using CRUDonMovieDB.Model;

namespace CRUDonMovieDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMovies(string? searchtxt, int? Id);
        Task<Movie> GetMovie(int Id);
        Task<int> CreateMovie(CreateMovieDto move);
        Task<int> UpdateMovie(UpdateMovieDto movie);
        Task<int> DeleteMovie(BaseModel.DeleteObj obj);

    }
}
