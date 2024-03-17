using System;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Phonebook.Models
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //--------------------------------------
        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM employees", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                EmployeeId = Convert.ToInt32(reader["employeeid"]),
                                DepartmentId = Convert.ToInt32(reader["departmentid"]),
                                Name = reader["name"].ToString(),
                                Position = reader["position"] != DBNull.Value ? reader["position"].ToString() : null,
                                Phone = reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null,
                                Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
        //--------------------------------------
        public void AddEmployee(int departmentId, string name, string position, string phone, string email)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO employees (departmentid, name, position, phone, email) VALUES (@DepartmentId, @Name, @Position, @Phone, @Email)", connection))
                {
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
        //--------------------------------------
        public void DeleteEmployee(int employeeId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM employees WHERE employeeid = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    command.ExecuteNonQuery();
                }
            }
        }
        //--------------------------------------
        public void UpdateEmployee(Employee updatedEmployee)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE employees SET name = @Name, position = @Position, phone = @Phone, email = @Email WHERE employeeid = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", updatedEmployee.EmployeeId);
                    command.Parameters.AddWithValue("@Name", updatedEmployee.Name);
                    command.Parameters.AddWithValue("@Position", updatedEmployee.Position ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", updatedEmployee.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", updatedEmployee.Email ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
        //--------------------------------------
        public Employee GetEmployeeById(int employeeId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM employees WHERE employeeid = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("employeeid")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentid")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString(reader.GetOrdinal("position")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"))
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        //--------------------------------------
        public IEnumerable<Employee> SearchEmployees(string searchString)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM employees WHERE name ILIKE @SearchString OR position ILIKE @SearchString OR phone ILIKE @SearchString OR email ILIKE @SearchString";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchString", $"%{searchString}%");
                    using (var reader = command.ExecuteReader())
                    {
                        var employees = new List<Employee>();
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("employeeid")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentid")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString(reader.GetOrdinal("position")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"))
                            });
                        }
                        return employees;
                    }
                }
            }
        }
        //--------------------------------------
        public IEnumerable<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            var employees = new List<Employee>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM employees WHERE departmentid = @departmentId", connection))
                {
                    command.Parameters.AddWithValue("departmentId", departmentId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("employeeid")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentid")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString(reader.GetOrdinal("position")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"))
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

    }
}
