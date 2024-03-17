using System;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Phonebook.Models
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //--------------------------------------
        public IEnumerable<Department> GetAllDepartments()
        {
            var departments = new List<Department>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM departments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var department = new Department
                            {
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentid")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ParentDepartmentId = reader.IsDBNull(reader.GetOrdinal("parentdepartmentid")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("parentdepartmentid"))
                            };
                            departments.Add(department);
                        }
                    }
                }
            }

            return departments;
        }
        //-----------------------------------
        public void AddDepartment(Department department)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO departments (name, parentdepartmentid) VALUES (@Name, @ParentDepartmentId)", connection))
                {
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@ParentDepartmentId", department.ParentDepartmentId.HasValue ? department.ParentDepartmentId.Value : (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }
        //----------------------------------
        public void DeleteDepartmentAndDescendantsRecursive(int departmentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // Удаляем всех детей данного подразделения
                DeleteChildDepartments(departmentId, connection);

                // Удаляем само подразделение
                using (var deleteCommand = new NpgsqlCommand("DELETE FROM departments WHERE departmentid = @DepartmentId", connection))
                {
                    deleteCommand.Parameters.AddWithValue("@DepartmentId", departmentId);
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }

        private void DeleteChildDepartments(int departmentId, NpgsqlConnection connection)
        {
            using (var command = new NpgsqlCommand("SELECT departmentid FROM departments WHERE parentdepartmentid = @DepartmentId", connection))
            {
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var childDepartmentId = reader.GetInt32(0);
                        // Удаляем дочернее подразделение и его потомков
                        DeleteDepartmentAndDescendantsRecursive(childDepartmentId);
                    }
                }
            }
        }

        //----------------------------------
        public void UpdateDepartment(Department department)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE departments SET name = @Name, parentdepartmentid = @ParentDepartmentId WHERE departmentid = @DepartmentId", connection))
                {
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@ParentDepartmentId", department.ParentDepartmentId.HasValue ? department.ParentDepartmentId.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);

                    command.ExecuteNonQuery();
                }
            }
        }
        //----------------------------------
        public Department GetDepartmentById(int departmentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM departments WHERE departmentid = @DepartmentId", connection))
                {
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Department
                            {
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("departmentid")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ParentDepartmentId = reader.IsDBNull(reader.GetOrdinal("parentdepartmentid")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("parentdepartmentid"))
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
        //----------------------------------
        public string GetDepartmentNameById(int departmentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT name FROM departments WHERE departmentid = @DepartmentId", connection))
                {
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    var departmentName = (string)command.ExecuteScalar();
                    return departmentName;
                }
            }
        }

    }
}
