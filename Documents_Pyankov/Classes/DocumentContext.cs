using Documents_Pyankov.Classes.Common;
using Documents_Pyankov.Interfaces;
using Documents_Pyankov.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Documents_Pyankov.Classes
{
    public class DocumentContext : Document, IDocument
    {
        public List<DocumentContext> AllDocuments()
        {
            List<DocumentContext> allDocuments = new List<DocumentContext>();

            OleDbConnection connection = DBConnection.Connection();
            OleDbDataReader dataDocuments = DBConnection.Query("SELECT * FROM [Документы]", connection);

            while (dataDocuments.Read())
            {
                allDocuments.Add(new DocumentContext()
                {
                    Id = dataDocuments["Код"] != DBNull.Value ? Convert.ToInt32(dataDocuments["Код"]) : 0,
                    Src = dataDocuments["Изображение"] != DBNull.Value ? dataDocuments["Изображение"].ToString() : string.Empty,
                    Name = dataDocuments["Наименование"] != DBNull.Value ? dataDocuments["Наименование"].ToString() : string.Empty,
                    User = dataDocuments["Ответственный"] != DBNull.Value ? dataDocuments["Ответственный"].ToString() : string.Empty,
                    IdDocument = dataDocuments["Код документа"] != DBNull.Value ? Convert.ToInt32(dataDocuments["Код документа"]) : 0,
                    Date = dataDocuments["Дата поступления"] != DBNull.Value ? dataDocuments["Дата поступления"].ToString() : string.Empty,
                    Status = dataDocuments["Статус"] != DBNull.Value ? Convert.ToInt32(dataDocuments["Статус"]) : 0,
                    Direction = dataDocuments["Направление"] != DBNull.Value ? dataDocuments["Направление"].ToString() : string.Empty,
                });
            }

            dataDocuments.Close();
            DBConnection.CloseConnection(connection);

            return allDocuments;
        }

        public void Delete()
        {
            OleDbConnection connection = DBConnection.Connection();
            DBConnection.Query($"DELETE FROM [Документы] WHERE [Код] = {this.Id}", connection);
            DBConnection.CloseConnection(connection);
        }

        public void Save(bool update = false)
        {
            OleDbConnection connection = DBConnection.Connection();

            try
            {
                if (update)
                {
                    string updateQuery = $@"UPDATE [Документы] 
                                          SET [Изображение] = '{this.Src?.Replace("'", "''")}', 
                                              [Наименование] = '{this.Name?.Replace("'", "''")}', 
                                              [Ответственный] = '{this.User?.Replace("'", "''")}', 
                                              [Код документа] = {this.IdDocument}, 
                                              [Дата поступления] = '{this.Date?.Replace("'", "''")}', 
                                              [Статус] = {this.Status}, 
                                              [Направление] = '{this.Direction?.Replace("'", "''")}' 
                                          WHERE [Код] = {this.Id}";

                    DBConnection.Query(updateQuery, connection);
                }
                else
                {
                    string insertQuery = $@"INSERT INTO [Документы] (
                                          [Изображение],
                                          [Наименование],
                                          [Ответственный],
                                          [Код документа],
                                          [Дата поступления],
                                          [Статус],
                                          [Направление])
                                          VALUES (
                                          '{this.Src?.Replace("'", "''")}',
                                          '{this.Name?.Replace("'", "''")}',
                                          '{this.User?.Replace("'", "''")}',
                                          {this.IdDocument},
                                          '{this.Date?.Replace("'", "''")}',
                                          {this.Status},
                                          '{this.Direction?.Replace("'", "''")}')";

                    DBConnection.Query(insertQuery, connection);
                }
            }
            finally
            {
                DBConnection.CloseConnection(connection);
            }
        }
    }
}