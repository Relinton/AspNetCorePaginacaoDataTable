
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAppCorePaginacaoDataTable.Models;

namespace WebAppCorePaginacaoDataTable.Controllers.api
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        [HttpPost]
        public IActionResult Post()
        {
            var requestFormData = Request.Form;

            List<Item> listaItens = BuscarItens();

            var listaItensForm = ProcessarDadosForm(listaItens, requestFormData);

            dynamic response = new
            {
                Data = listaItensForm,
                Draw = requestFormData["draw"],
                RecordsFiltered = listaItens.Count,
                RecordsTotal = listaItens.Count
            };
            return Ok(response);
        }

        //Lista de Items estática, para ser retornada.
        private List<Item> BuscarItens()
        {
            List<Item> listaDeItens = new List<Item>()
        {
            new Item(){ItemId = 1, Nome = "Sistemas de Informação", Descricao = "Curso para Desenvolvimento de Sistemas"},
            new Item(){ItemId = 2, Nome = "Direito", Descricao = "Curso para formação de profissionais na área jurídica"},
            new Item(){ItemId = 3, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
            new Item(){ItemId = 4, Nome = "Direito", Descricao = "Curso para formação de profissionais na área jurídica"},
            new Item(){ItemId = 5, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
            new Item(){ItemId = 6, Nome = "Direito", Descricao = "Curso para formação de profissionais na área jurídica"},
            new Item(){ItemId = 7, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
            new Item(){ItemId = 8, Nome = "Direito", Descricao = "Curso para formação de profissionais na área jurídica"},
            new Item(){ItemId = 9, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
            new Item(){ItemId = 10, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
            new Item(){ItemId = 11, Nome = "Direito", Descricao = "Curso para formação de profissionais na área jurídica"},
            new Item(){ItemId = 12, Nome = "Medicina", Descricao = "Curso para formação de profissionais que cuida da vida"},
        };
            return listaDeItens;
        }


        private List<Item> ProcessarDadosForm(List<Item> lstElements, IFormCollection requestFormData)
        {
            var skip = Convert.ToInt32(requestFormData["start"].ToString());
            var pageSize = Convert.ToInt32(requestFormData["length"].ToString());
            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] { "" };

            if (requestFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var columnIndex = requestFormData["order[0][column]"].ToString();
                var sortDirection = requestFormData["order[0][dir]"].ToString();
                tempOrder = new[] { "" };
                if (requestFormData.TryGetValue($"columns[{columnIndex}][data]", out tempOrder))
                {
                    var columName = requestFormData[$"columns[{columnIndex}][data]"].ToString();

                    if (pageSize > 0)
                    {
                        var prop = getProperty(columName);
                        if (sortDirection == "asc")
                        {
                            return lstElements.OrderBy(prop.GetValue).Skip(skip)
                                .Take(pageSize).ToList();
                        }
                        else
                            return lstElements.OrderByDescending(prop.GetValue)
                                .Skip(skip).Take(pageSize).ToList();
                    }
                    else
                        return lstElements;
                }
            }
            return null;
        }

        private PropertyInfo getProperty(string name)
        {
            var properties = typeof(Models.Item).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            return prop;
        }
    }
}
