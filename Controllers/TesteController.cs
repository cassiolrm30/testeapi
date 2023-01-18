using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAPI.Interfaces;
using TesteAPI.Models;
using TesteAPI.ViewModels;

namespace TesteAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ITesteRepository _repositorio;
        private readonly IMapper _mapper;

        public TesteController(ITesteRepository repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<TesteViewModel>> Get()
        {
            var resultado = new List<TesteViewModel>();
            var registros = await _repositorio.GetAll();
            foreach (var item in registros)
            {
                resultado.Add(new TesteViewModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Valor = item.Valor.ToString(),
                    Data = item.Data.ToString("yyyy-MM-dd"),
                    Ativo = item.Ativo ? "Ativo" : "Inativo",
                    Opcao = item.Opcao.Descricao
                });
            }
            return _mapper.Map<IList<TesteViewModel>>(resultado);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TesteViewModel>> Get(int id)
        {
            var registro = await _repositorio.GetById(id);
            if (registro == null) return NotFound();
            var resultado = new TesteViewModel()
            {
                Id = registro.Id,
                Descricao = registro.Descricao,
                Valor = registro.Valor.ToString(),
                Data = registro.Data.ToString("yyyy-MM-dd"),
                Ativo = registro.Ativo ? "Ativo" : "Inativo",
                Opcao = registro.Opcao.Id.ToString()
            };
            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<TesteViewModel>> Post(TesteViewModel model)
        {
            if (!ModelState.IsValid) return NotFound(new { sucesso = false });
            var registro = new Teste()
            {
                Id = 0,
                Descricao = model.Descricao,
                Valor = decimal.Parse(model.Valor),
                Data = DateTime.Parse(model.Data),
                Ativo = model.Ativo.Equals("Ativo"),
                OpcaoId = int.Parse(model.Opcao)
            };
            await _repositorio.Post(registro);
            return Ok(new { id = registro.Id, sucesso = true });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TesteViewModel>> Put(int id, TesteViewModel model)
        {
            if (id < 1 || model.Id != id || !ModelState.IsValid) return NotFound(new { sucesso = false });
            var registroAtual = await _repositorio.GetById(id);
            var autor = new Teste()
            {
                Id = id,
                Descricao = model.Descricao,
                Valor = decimal.Parse(model.Valor),
                Data = DateTime.Parse(model.Data),
                Ativo = model.Ativo.Equals("Ativo"),
                OpcaoId = int.Parse(model.Opcao)
            };
            await _repositorio.Put(autor);
            return Ok(new { id = autor.Id, sucesso = true });
        }

        [HttpDelete("excluir/{id:int}")]
        public async Task<ActionResult<TesteViewModel>> Delete(int id)
        {
            var model = await _repositorio.GetById(id);
            if (model == null) return NotFound(new { sucesso = false });
            await _repositorio.Delete(model);
            return Ok(new { id = model.Id, sucesso = true });
        }
    }
}