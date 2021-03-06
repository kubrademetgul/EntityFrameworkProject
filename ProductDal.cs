using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp28
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb; initial catalog = ETrade; integrated security  = true");
        public List<Product> GetAll()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from Product", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();
            while(reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name=reader["Name"].ToString(),
                    StockAmount=Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice =Convert.ToDecimal(reader["UnitPrice"])

                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;

        }
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public DataTable GetAll2()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlCommand command = new SqlCommand("Select * from Product", _connection);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            _connection.Close();
            return dataTable;

        }
        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Products values(@name,@unitPrice,@stockAmount)", _connection);
            command.Parameters.AddWithValue("@name",product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
           

            _connection.Close();
        }
        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Update Products set Name=@name,UnitPrice=@unitPrice,StockAmount=@stockAmount where Id=@id)", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
        _connection.Close();
        }

    }
}
