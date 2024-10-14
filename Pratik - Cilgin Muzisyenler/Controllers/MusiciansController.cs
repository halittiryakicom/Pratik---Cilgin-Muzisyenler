using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Pratik___Cilgin_Muzisyenler.Models;
using static System.Net.WebRequestMethods;

namespace Pratik___Cilgin_Muzisyenler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {

        //Here we defined the static list of musicians
        private static List<Musician> _musicians = new List<Musician>
        {
            new Musician
            {
                Id = 1,
                Name = "Ahmet Çalgı",
                Job = "Ünlü Çalgı Çalar",
                FunProperty = "Her zaman yanlış nota çalar ama çok eğlenceli"
            },

            new Musician
            {
                Id = 2,
                Name = "Zeynep Melodi",
                Job = "Popüler Melodi Yazarı",
                FunProperty = "Şarkıları yanlış anlaşılır ama çok popüler"
            },

            new Musician
            {
                Id = 3,
                Name = "Cemil Akor",
                Job = "Çılgın Akorist",
                FunProperty = "Akorları sık değiştirir ama şaşırtıcı derecede yetenekli"
            },

            new Musician
            {
                Id = 4,
                Name = "Fatma Nota",
                Job = "Süpriz Nota Üreticisi",
                FunProperty = "Nota üretirken sürekli süprizler hazırlar"
            },

            new Musician
            {
                Id = 5,
                Name = "Hasan Ritim",
                Job = "Ritim Canavarı",
                FunProperty = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir."
            },

            new Musician
            {
                Id = 6,
                Name = "Elif Armoni",
                Job = "Armoni Ustası",
                FunProperty = "Armonilerini bazen yanlış çalar ama çok üretkendir."
            },

            new Musician
            {
                Id = 7,
                Name = "Ali Perde",
                Job = "Perde Uygulayıcı",
                FunProperty = "Her perde farklı şekilde çalar ama çok üretken"
            },

            new Musician
            {
                Id = 8,
                Name = "Ayşe Rezonans",
                Job = "Rezonans Uzmanı",
                FunProperty = "Rezonans konusunda uzman ama bazen çok gürültü çıkarır"
            },

            new Musician
            {
                Id = 9,
                Name = "Murat Ton",
                Job = "Tonlama Meraklısı",
                FunProperty = "Tonlamalarda ki farklılıkları bazen komik ama oldukça ilginç"
            },

            new Musician
            {
                Id = 10,
                Name = "Selin Akor",
                Job = "Akor Sihirbazı",
                FunProperty = "Akorları değiştirildiğinde bazen sihirli bir hava getirir"
            },
        };

        //We called the list of all musicians using the get command
        [HttpGet]
        public IEnumerable<Musician> GetAll()
        {
            return _musicians;
        }

        //Here we called the musician by id
        [HttpGet("{id}")]
        public ActionResult<Musician> Get(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return Ok(musician);
        }

        //We created a new musician with http post
        [HttpPost]
        public ActionResult<Musician> Post([FromBody] Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            musician.Id = _musicians.Max(x => x.Id) + 1;
            _musicians.Add(musician);
            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
        }

        //Eğlence Özelliği özelliğini HttpPatch ile güncelledik
        [HttpPatch("reschedule/{id:int:min(1)}/{newFunProperty}")]
        public ActionResult RescheduleTour(int id, string newFunProperty)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician is null)
            {
                return NotFound($"Musician id {id} not found");
            }
            musician.FunProperty = newFunProperty;
            return NoContent();
        }

        //Musician name updated with httpput
        [HttpPut("update/{id:int:min(1)}/{musicianName}")]
        public IActionResult UpdateTourPlanet(int id, string musicianName)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician is null)
            {
                return NotFound($"Musician id {id} bulunamadı");
            }

            musician.Name = musicianName;
            return NoContent();
        }


        //We delete both id and name with httpdelete
        [HttpDelete("cancel/{id:int:min(1)}")]
        [HttpDelete("cancel/{musicianName}")]
        public IActionResult CancelTour(int? id, string? musicianName)
        {
            Musician musicianToRemove;

            if (id.HasValue)
            {
                musicianToRemove = _musicians.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                musicianToRemove = _musicians.FirstOrDefault(x => x.Name.Equals(musicianName, StringComparison.OrdinalIgnoreCase));
            }

            if (musicianToRemove is null)
            {
                return NotFound("Belirtilen tur bulunamadı");
            }
            _musicians.Remove(musicianToRemove);
            return NoContent();
        }

        //We searched by name, profession and identity.
        [HttpPost("complex-search")]
        public ActionResult<IEnumerable<Musician>> ComplexSearch([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? job)
        {
            var musicians = _musicians.AsQueryable();

            // Name parametresi varsa filtreleme
            if (!string.IsNullOrWhiteSpace(name))
            {
                musicians = musicians.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            // Job parametresi varsa filtreleme
            if (!string.IsNullOrWhiteSpace(job))
            {
                musicians = musicians.Where(x => x.Job.Equals(job, StringComparison.OrdinalIgnoreCase));
            }

            // Id parametresi varsa filtreleme
            if (id.HasValue)
            {
                musicians = musicians.Where(x => x.Id == id);
            }

            return Ok(musicians.ToList());
        }
    }
}
