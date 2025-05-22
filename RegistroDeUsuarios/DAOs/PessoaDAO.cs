using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RegistroDeUsuarios.Infra.DataBase;
using RegistroDeUsuarios.Models;
namespace RegistroDeUsuarios.DAOs
{
    public class PessoaDAO
    {
        private ConnectDatabase dbConnection;
        public PessoaDAO()
        {
            try { 
                dbConnection = new ConnectDatabase();
            } catch(Exception e) {
                MessageBox.Show(e.Message);
            }
        }
        public void Create(Pessoa pessoa)
        {
            string Sql = "INSERT INTO pessoas" + "(name, email, phone) VALUES (@n, @e, @p)";
            try
            {
                var cmd = dbConnection.Connect().CreateCommand();
                cmd.CommandText = Sql;
                cmd.Parameters.AddWithValue("@n", pessoa.Name);
                cmd.Parameters.AddWithValue("@e", pessoa.Email);
                cmd.Parameters.AddWithValue("@p", pessoa.Phone);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally { 
                dbConnection.Desconnect();
            }
        }
        public List<Pessoa> GetAll()
        {
            string sql = "SELECT * FROM PESSOAS";
            List<Pessoa> listaPessoa = new List<Pessoa>();
            try
            {
                var cmd = dbConnection.Connect().CreateCommand();
                cmd.CommandText = sql;
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Pessoa pessoa = new Pessoa();
                    pessoa.ID = result.GetInt32(0);
                    pessoa.Name = result.GetString(1);
                    pessoa.Email = result.GetString(2);
                    pessoa.Phone = result.GetString(result.GetOrdinal("Phone"));
                    listaPessoa.Add(pessoa);
                }
                result.Close();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            finally {
                dbConnection.Desconnect();
            }
            return listaPessoa;
        }
        
        public Pessoa GetById(int id)
        {
            string sql = "SELECT * FROM PESSOAS WHERE ID = @id";
            Pessoa pessoa = null;
            try
            {
                var cmd = dbConnection.Connect().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                var result = cmd.ExecuteReader();
                if (result.Read())
                {
                    pessoa = new Pessoa();
                    pessoa.ID = result.GetInt32(0);
                    pessoa.Name = result.GetString(1);
                    pessoa.Email = result.GetString(2);
                    pessoa.Phone = result.GetString(result.GetOrdinal("Phone"));
                }
                result.Close();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            finally {
                dbConnection.Desconnect();
            }
            return pessoa;
        }
        
        public void Update(Pessoa pessoa)
        {
            string sql = "UPDATE PESSOAS SET name = @n, email = @e, phone = @p WHERE id = @id";
            try
            {
                var cmd = dbConnection.Connect().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@n", pessoa.Name);
                cmd.Parameters.AddWithValue("@e", pessoa.Email);
                cmd.Parameters.AddWithValue("@p", pessoa.Phone);
                cmd.Parameters.AddWithValue("@id", pessoa.ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally {
                dbConnection.Desconnect();
            }
        }
        
        public void Delete(int id)
        {
            string sql = "DELETE FROM PESSOAS WHERE id = @id";
            try
            {
                var cmd = dbConnection.Connect().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally {
                dbConnection.Desconnect();
            }
        }
    }
}
