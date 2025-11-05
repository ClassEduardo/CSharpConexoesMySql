using System.Linq.Expressions;
using ConexoesMySql.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ConexoesMySql.Conexoes.EntityFramework;
public class EntityController(AppDbContext _context) : ControllerBase
{
    private readonly AppDbContext context = _context;
}