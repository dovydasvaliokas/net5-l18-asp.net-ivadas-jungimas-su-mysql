using MySql.Data.MySqlClient;

namespace ViltiesZiedasFilmai.Models
{
    public class VZContext
    {
        public string ConnectionString { get; set; }

        public VZContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GautiJungti()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Film> GautiVisusFilmus()
        {
            List<Film> list = new List<Film>();

            using (MySqlConnection jungtis = GautiJungti())
            {
                jungtis.Open();
                MySqlCommand sqlUzklausa = new MySqlCommand("SELECT * FROM film", jungtis);

                using (MySqlDataReader skaitytuvas = sqlUzklausa.ExecuteReader())
                {
                    while (skaitytuvas.Read())
                    {
                        Film laikinasFilmas = new Film()
                        {
                            FilmId = skaitytuvas.GetInt32("film_id"),
                            Title = skaitytuvas.GetString("title"),
                            Description = skaitytuvas.GetString("description"),
                            ReleaseYear = skaitytuvas.GetInt32("release_year"),
                            Length = skaitytuvas.GetInt32("length"),
                            Rating = skaitytuvas.GetString("rating"),
                        };

                        list.Add(laikinasFilmas);
                        
                    }
                }
            }
            return list;
        }
    }
}
