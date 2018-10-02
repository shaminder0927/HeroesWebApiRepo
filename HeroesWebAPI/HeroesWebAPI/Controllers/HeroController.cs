using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Http;
using HeroesWebAPI.Models;

namespace HeroesWebAPI.Controllers
{
    public class HeroController : ApiController
    {
        HeroApi[] heroes = new HeroApi[]
        {
          new HeroApi {id= 11, name="Super Man", power="Is Strongest" },
          new HeroApi {id= 12, name="Batman", power="Intelligent" },
          new HeroApi {id= 13, name="Flash", power="has speed" },
          new HeroApi {id= 14, name="Hulk", power="powerhouse of strength" },
          new HeroApi {id= 15, name= "Spiderman", power= "has speed and can fly" },
          new HeroApi {id= 16, name= "Thor", power= "has thunder power" },
          new HeroApi {id= 17, name= "Rock", power= "can smash" },
          new HeroApi {id= 19, name= "Hulk", power= "is super strong"}
        };

        public IEnumerable<HeroApi> GetAllHeroes() {
            return heroes;
        }

        public IHttpActionResult GetHero(int id)
        {
            var hero = heroes.FirstOrDefault(p => p.id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
    }
}
