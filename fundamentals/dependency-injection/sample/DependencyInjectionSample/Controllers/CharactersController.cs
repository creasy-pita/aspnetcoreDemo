using System.Linq;
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionSample.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _characterRepository;
        public CharactersController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        // GET: /characters/
        public IActionResult Index()
        {


            PopulateCharactersIfNoneExist();
            var characters = _characterRepository.ListAll();

            return View(characters);
        }
        
        private void PopulateCharactersIfNoneExist()
        {
            //if (!_characterRepository.ListAll().Any())
            //{
            //    if (_characterRepository.GetType() == typeof(MysqlCharacterRepository))
            //    {
            //        _characterRepository.Add(new Character("mysql Darth Maul"));
            //        _characterRepository.Add(new Character("mysqlDarth Vader"));
            //        _characterRepository.Add(new Character("mysqlmysqlYoda"));
            //        _characterRepository.Add(new Character("mysqlMace Windu"));
            //    }
            //    else
            //    {
            //        _characterRepository.Add(new Character("Darth Maul"));
            //        _characterRepository.Add(new Character("Darth Vader"));
            //        _characterRepository.Add(new Character("Yoda"));
            //        _characterRepository.Add(new Character("Mace Windu"));
            //    }
            //}
        }
    }
}
