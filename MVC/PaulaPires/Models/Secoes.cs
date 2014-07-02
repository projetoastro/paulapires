﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBFactory;

namespace PaulaPires.Models
{
    public class Secoes
    {
        #region :: Attributes and Properties ::

        public int Id { get; set; }
        public string Nome { get; set; }        

        #endregion
        #region :: Constructors ::

        public Secoes()
        {
            Clear();
        }

        public Secoes(string pUsuario, string pSenha)
        {
            Clear();
            Load(pUsuario, pSenha);
        }

        private Secoes(DataRow pRow)
        {
            Clear();
            FillAttributes(pRow);
        }

        #endregion
        #region :: Methods ::

        public void Clear()
        {
            Id = 0;
            Nome = string.Empty;            
        }

        public void Load(string pUsuario, string pSenha)
        {
            SqlParameter[] sqlParametros = new SqlParameter[]{
                new SqlParameter("@username", pUsuario),
                new SqlParameter("@password", pSenha)
            };

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select("spValidaLogin", CommandType.StoredProcedure, sqlParametros);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
                FillAttributes(DataHelper.GetFirstRow(result));
        }

        public void FillAttributes(DataRow pRow)
        {
            if (pRow.Table.Columns.Contains("Id")) { Id = int.Parse(pRow["Id"].ToString()); }
            if (pRow.Table.Columns.Contains("Nome")) { Nome = Convert.ToString(pRow["Nome"].ToString()); }
        }

        #endregion
        #region :: Static Methods ::

        public static List<Secoes> List()
        {
            return GenericList("spListSecoes");
        }

        public static List<Secoes> GenericList(string pQuery)
        {
            var list = new List<Secoes>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Secoes(item));
            }

            return list;
        }

        public static List<Secoes> GenericList(string pQuery, SqlParameter[] pSqlParametro)
        {
            var list = new List<Secoes>();

            SQLServer sql = new SQLServer();
            DataSet result = sql.Select(pQuery, CommandType.StoredProcedure, pSqlParametro);

            if (DataHelper.HasTable(result) && DataHelper.HasRows(result))
            {
                foreach (DataRow item in DataHelper.GetRows(result))
                    list.Add(new Secoes(item));
            }

            return list;
        }

        #endregion
    }
}