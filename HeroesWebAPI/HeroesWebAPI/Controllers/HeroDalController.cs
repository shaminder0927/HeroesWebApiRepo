using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HeroesDAL;

namespace HeroesWebAPI.Controllers
{
    public class HeroDalController : ApiController
    {
        //GET api/<controller>
        public IEnumerable<Models.HeroApi> GetAllHeroes()
        {
            using (var context = new HeroesDalEntities())
            {
                return MapFromDAL(context.Heroes.ToList());
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult GetHero(int id)
        {
            using (var context = new HeroesDalEntities())
            {
                var hero = context.Heroes.ToList().FirstOrDefault(p => p.Id == id);
                if (hero == null)
                {
                    return NotFound();
                }
                return Ok(MapFromDAL(hero));
            }
        }

        [Route("api/HeroDal/SearchHeroes")]
        [HttpGet]
        // Get Searched Heroes
        public IHttpActionResult SearchHeroes(string term)
        {
            using (var context = new HeroesDalEntities())
            {
                var hero = (from h in context.Heroes
                           where h.Name.Contains(term)
                           select h).ToList();

                if (hero == null)
                {
                    return NotFound();
                }
                return Ok(MapFromDAL(hero));
            }
        }

        [Route("api/HeroDal/PostHero")]
        [HttpPost]
        public IHttpActionResult PostHero([FromBody] Models.HeroApi heroApi)
        {
            using (var context = new HeroesDalEntities())
            {
                Hero heroDB = null;
                if (heroApi.id == 0) //Add hero
                {
                    heroDB = new Hero();
                    heroDB = MapToDAL(heroApi, heroDB);

                    context.Heroes.Add(heroDB);
                }
                else
                {
                    heroDB = context.Heroes.ToList().FirstOrDefault((p) => p.Id == heroApi.id);
                    heroDB = MapToDAL(heroApi, heroDB);
                }
                context.SaveChanges();

                return Ok(heroDB);
            }
        }

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            using (var context = new HeroesDalEntities())
            {
                var hero = context.Heroes.ToList().FirstOrDefault(p => p.Id == id);
                if (hero == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Heroes.Remove(hero);
                    context.SaveChanges();

                    return Ok(MapFromDAL(hero));
                }
            }
        }
        
        public List<Models.HeroApi> MapFromDAL(List<Hero> hero)
        {
            return hero.Select(x => MapFromDAL(x)).ToList();
        }

        // Mapping Data from HeroesDal to HeroesWebApi HeroWebApi class
        public Models.HeroApi MapFromDAL(Hero hero)
        {
            return new Models.HeroApi()
            {
                id = hero.Id,
                name = hero.Name,
                power = hero.Power
            };
        }

        // Mapping Data from HeroesWebAPI to HeroesDal Hero class 
        public Hero MapToDAL(Models.HeroApi heroWebApi , Hero hero)
        {
            if (hero == null)
            {
                hero = new Hero();
            }
            hero.Id = heroWebApi.id;
            hero.Name = heroWebApi.name;
            hero.Power = heroWebApi.power;

            return hero;
        }
    }
}