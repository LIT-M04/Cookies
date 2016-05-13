using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Data
{
    public class PeopleManager
    {
        private string _connectionString;

        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People";
                connection.Open();
                return GetFromCommand(command);
            }
        }

        public IEnumerable<Person> SearchByLastName(string lastName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People WHERE LastName LIKE @lastName";
                command.Parameters.AddWithValue("@lastName", "%" + lastName + "%");
                connection.Open();
                return GetFromCommand(command);
            }
        }

        private IEnumerable<Person> GetFromCommand(SqlCommand command)
        {
            List<Person> people = new List<Person>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person person = new Person();
                person.Id = (int)reader["Id"];
                person.FirstName = (string)reader["FirstName"];
                person.LastName = (string)reader["LastName"];
                person.Age = (int)reader["Age"];
                people.Add(person);
            }

            return people;
        } 
    }
}
