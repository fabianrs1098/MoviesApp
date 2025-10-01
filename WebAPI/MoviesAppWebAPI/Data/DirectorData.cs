using MoviesAppWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MoviesAppWebAPI.Data
{
    public class DirectorData
    {
        private readonly string conexion;

        public DirectorData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Director>> Get()
        {
            List<Director> lista = new List<Director>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Director", con);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Director
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Nationality = reader["Nationality"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Active = Convert.ToBoolean(reader["Active"].ToString())
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Director> GetById(int id)
        {
            Director objeto = new Director();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Director WHERE Id = {id}", con);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Director
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Nationality = reader["Nationality"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Active = Convert.ToBoolean(reader["Active"].ToString())
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Create(Director objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand($"INSERT INTO Director VALUES ('{objeto.Name}','{objeto.Nationality}',{objeto.Age},{Convert.ToInt32(objeto.Active)})", con);
                
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

        public async Task<bool> Update(Director objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand($"UPDATE Director SET Name = '{objeto.Name}', Nationality = '{objeto.Nationality}', Age = {objeto.Age}, Active = {Convert.ToInt32(objeto.Active)} WHERE Id = {objeto.Id}", con);

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

                SqlCommand cmd = new SqlCommand($"DELETE Director WHERE Id = {id}", con);

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
