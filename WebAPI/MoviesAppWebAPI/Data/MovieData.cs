using MoviesAppWebAPI.Models;
using System.Data.SqlClient;

namespace MoviesAppWebAPI.Data
{
    public class MovieData
    {
        private readonly string conexion;

        public MovieData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Movie>> Get()
        {
            List<Movie> lista = new List<Movie>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Movies", con);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ReleaseYear = Convert.ToDateTime(reader["ReleaseYear"].ToString()),
                            Gender = reader["Gender"].ToString(),
                            Duration = TimeSpan.Parse(reader["Duration"].ToString()),
                            FKDirector = reader["FKDirector"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Movie> GetById(int id)
        {
            Movie objeto = new Movie();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Movies WHERE Id = {id}", con);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ReleaseYear = Convert.ToDateTime(reader["ReleaseYear"].ToString()),
                            Gender = reader["Gender"].ToString(),
                            Duration = TimeSpan.Parse(reader["Duration"].ToString()),
                            FKDirector = reader["FKDirector"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Create(Movie objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand($"INSERT INTO Movies VALUES ('{objeto.Name}','{objeto.ReleaseYear.ToString("yyyy-MM-dd")}','{objeto.Gender}','{objeto.Duration.ToString("HH:mm:ss")}','{objeto.FKDirector}'", con);

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Update(Movie objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand($"UPDATE Movies SET Name = '{objeto.Name}', ReleaseYear = '{objeto.ReleaseYear.ToString("yyyy-MM-dd")}', Gender = '{objeto.Gender}', Duration = '{objeto.Duration.ToString("HH:mm:ss")}', FKDirector = '{objeto.FKDirector}' WHERE Id = {objeto.Id}", con);

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Delete(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand($"DELETE Movies WHERE Id = {id}", con);

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }
    }
}
