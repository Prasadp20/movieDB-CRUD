using System.Data.Common;
using CRUDonMovieDB.Model;
using CRUDonMovieDB.Repository.Interfaces;
using Dapper;

namespace CRUDonMovieDB.Repository
{
    public class MovieRepository : BaseAsyncRepository, IMovieRepository
    {
        public MovieRepository(IConfiguration _con) : base(_con)
        {

        }

        public async Task<int> CreateMovie(CreateMovieDto move)
        {
            int res = 0;
            var sql = " insert into movie (mov_title, mov_year, mov_time, mov_lang, mov_dt_rel," +
                      " mov_rel_country, createdBy, createDate, modifiedBy, modifiedDate, isDeleted)" +
                      " values (@mov_title, @mov_year, @mov_time, @mov_lang, @mov_dt_rel," +
                      " @mov_rel_country, 111, CURRENT_TIMESTAMP, 222, CURRENT_TIMESTAMP, 0) " +
                      " SELECT CAST(SCOPE_IDENTITY() as int) ";

            using(DbConnection db = ReaderConnectionString)
            {
                await db.OpenAsync();
                res = await db.QuerySingleAsync<int>(sql, move);

                if(res != 0)
                {
                    var result = await AddActor(move.actor);
                    var result1 = await AddDirector(move.direct);
                    var result2 = await AddGeners(move.geners);
                    var result3 = await AddRatingReviewer(move.rate);
                }
                return res;
            }
        }

        private async Task<int> AddRatingReviewer(List<MovieRatingReviewer> rate)
        {
            int result = 0;
            using(DbConnection db = WriterConnectionString)
            {
                await db.OpenAsync();
                if(rate.Count > 0)
                {
                    foreach(MovieRatingReviewer rating in rate)
                    {
                        //rating.rev_id = revId;

                        var sql = " insert into movie_rating_reviwer (rev_id, rev_stars, num_o_ratings, rev_name)" +
                                  " values (@rev_id, @rev_stars, @num_o_ratings, @rev_name) ";

                        var result1 = await db.ExecuteAsync(sql, rating);

                        result = result + result1;
                    }
                }
                return result;
            }
        }

        private async Task<int> AddGeners(List<MovieGeners> geners)
        {
            int result = 0;
            using(DbConnection db = WriterConnectionString)
            {
                if(geners.Count > 0)
                {
                    foreach(MovieGeners glistener in geners)
                    {
                        //glistener.gen_id = genId;

                        var sql = " insert into movie_geners (gen_id, gen_title) " +
                                  " values (@gen_id, @gen_title) ";

                        var result1 = await db.ExecuteAsync(sql, glistener);
                        result = result + result1;
                    }
                }
                return result;
            }
        }

        private async Task<int> AddDirector(List<MovieDirector> direct)
        {
            int result = 0;
            using(DbConnection db = WriterConnectionString)
            {
                await db.OpenAsync();
                if(direct.Count > 0)
                {
                    foreach(MovieDirector director in direct)
                    {
                        //director.dir_id = dirId;

                        var sql = " insert into movie_director (dir_id, dir_fname, dir_lname) " +
                                  " values (@dir_id, @dir_fname, @dir_lname) ";

                        var result1 = await db.ExecuteAsync(sql, director);

                        result = result + result1;
                    }
                }
                return result;
            }
        }

        private async Task<int> AddActor(List<MovieActor> act)
        {
            int result = 0;
            using(DbConnection db = WriterConnectionString)
            {
                await db.OpenAsync();
                if(act.Count > 0)
                {
                    foreach(MovieActor actor in act)
                    {
                        //actor.act_id = actId;

                        var sql = " insert into movie_actor (act_id, act_fname, act_lname, act_gender) " +
                                  " values (@act_id, @act_fname, @act_lname, @act_gender) ";

                        var result1 = await db.ExecuteAsync(sql, actor);

                        result = result + result1;
                    }
                }
                return result;
            }
        }

        public async Task<List<Movie>> GetAllMovies(string? searchtxt, int? Id)
        {
            var sql = @"  select mo.Id, mo.mov_title, mo.mov_year, mo.mov_time, mo.mov_lang, 
                          mo.mov_dt_rel, mo.mov_rel_country,
                          mo.createdBy, mo.createDate, mo.modifiedBy, mo.modifiedDate,
                          ac.act_fname+' '+ac.act_lname as ActorName, ac.role, 
                          md.dir_fname+' '+md.dir_lname as DirectorName, 
                          mg.gen_id, mg.gen_title, 
                          mrr.rev_stars, mrr.num_o_ratings, mrr.rev_name
                          from movie_actor ac join movie mo
                          on ac.act_id = mo.act_id join movie_director md
                          on md.dir_id = mo.dir_id join movie_geners mg
                          on mg.gen_id = mo.gen_id join movie_rating_reviwer mrr
                          on mrr.rev_id = mo.rev_id
                          where mo.Id = @Id or (@Id = 0) AND 
                          mo.mov_title = '%'+@searchtxt+'%' or (@searchtxt IS NULL) ";

            using(DbConnection db = ReaderConnectionString)
            {
                await db.OpenAsync();
                var res = await db.QueryAsync<Movie>(sql, new { searchtxt = searchtxt, Id = Id });

                return res.ToList();
            }
        }

        public async Task<Movie> GetMovie(int Id)
        {
            var sql = @"select mo.Id, mo.mov_title, mo.mov_year, mo.mov_time, mo.mov_lang, 
                          mo.mov_dt_rel, mo.mov_rel_country,
                          mo.createdBy, mo.createDate, mo.modifiedBy, mo.modifiedDate,
                          ac.act_fname+' '+ac.act_lname as ActorName, ac.role, 
                          md.dir_fname+' '+md.dir_lname as DirectorName, 
                          mg.gen_title, 
                          mrr.rev_stars, mrr.num_o_ratings, mrr.rev_name
                          from movie_actor ac join movie mo
                          on ac.act_id = mo.act_id join movie_director md
                          on md.dir_id = mo.dir_id join movie_geners mg
                          on mg.gen_id = mo.gen_id join movie_rating_reviwer mrr
                          on mrr.rev_id = mo.rev_id
                          where mo.Id = @Id ";

            using(DbConnection db = ReaderConnectionString)
            {
                await db.OpenAsync();

                var res = await db.QuerySingleAsync<Movie>(sql, new { id = Id });
                return res;
            }
        }

        public async Task<int> UpdateMovie(UpdateMovieDto movie)
        {
            int result = 0;
            var sql = "  update movie set " +
                      "  mov_title = @mov_title," +
                      "  mov_year = @mov_year," +
                      "  mov_time = @mov_time," +
                      "  mov_lang = @mov_lang," +
                      "  mov_dt_rel = @mov_dt_rel," +
                      "  mov_rel_country = @mov_rel_country," +
                      "  modifiedBy = 1010," +
                      "  modifiedDate = CURRENT_TIMESTAMP," +
                      "  isDeleted = @isDeleted" +
                      "  where Id = @Id ";

            using (DbConnection db = WriterConnectionString)
            {
                await db.OpenAsync();
                result = await db.ExecuteAsync(sql, movie);

                if (result != 0)
                {
                    result = await db.ExecuteAsync(@"delete from movie_actor where act_id = @Id", movie);
                    var result1 = await AddActor(movie.actor);

                    result = await db.ExecuteAsync(@"delete from movie_director where dir_id = @Id", movie);
                    var result2 = await AddDirector(movie.direct);

                    result = await db.ExecuteAsync(@"delete from movie_geners where gen_id = @Id", movie);
                    var result3 = await AddGeners(movie.geners);

                    result = await db.ExecuteAsync(@"delete from movie_rating_reviwer where rev_id = @Id", movie);
                    var result4 = await AddRatingReviewer(movie.rate);

                }
                return result;
            }
        }

        public async Task<int> DeleteMovie(BaseModel.DeleteObj obj)
        {

            int res = 0;
            var sql = " update movie set isDeleted = 1, modifiedBy=1020, " +
                      " modifiedDate = CURRENT_TIMESTAMP" +
                      " where Id = @Id ";

            using(DbConnection db = WriterConnectionString)
            {
                await db.OpenAsync();
                res = await db.ExecuteAsync(sql, obj);

                return res;
            }
        }
    }
}
