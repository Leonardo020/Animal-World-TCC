using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class AnimalController : Controller
    {
        actionsAnimal acAnimal = new actionsAnimal();

        Conexao cn = new Conexao();

        public void carregaEspecies()
        {
            List<SelectListItem> especies = acAnimal.carregaEspecie();
            ViewBag.especies = new SelectList(especies, "Value", "Text");
        }

        void carregaRacas()
        {
            List<SelectListItem> racas = acAnimal.carregaRacas();
            ViewBag.racas = new SelectList(racas, "Value", "Text");
        }

        void carregaPortes()
        {
            List<SelectListItem> portes = acAnimal.carregaPortes();
            ViewBag.portes = new SelectList(portes, "Value", "Text");
        }

        [HttpPost]
        public ActionResult obterRacasByEspecie(int idEspecie)
        {
            var racas = acAnimal.consultaRacasPorEspecie(idEspecie);
            return Json(racas);
        }

        public ActionResult CadastroPet()
        {
            carregaEspecies();
            carregaRacas();
            carregaPortes();
            return View();
        }

        [HttpPost]
        public ActionResult CadastroPet(Animal animal, HttpPostedFileBase file, FormCollection frm)
        {
            carregaEspecies();
            carregaRacas();
            carregaPortes();
            try
            {
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/img/" + arquivo;
                string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                file.SaveAs(path);
                animal.codCli = Convert.ToInt32(Session["Cliente"]);
                animal.codEspecie = Convert.ToInt32(Request["especies"]);
                animal.codRaca = Convert.ToInt32(Request["racas"]);
                animal.codPorte = Convert.ToInt32(Request["portes"]);
                animal.descricaoAnimal = frm["descAnimal"];
                animal.imagemAnimal = file2;

                acAnimal.cadastraAnimal(animal);
                TempData["success"] = "O cadastro foi efetuado com sucesso!";
            }

            catch
            {
                TempData["error"] = "Erro ao cadastrar pet";
            }
            return View();
        }

        public ActionResult DadosPet(int id)
        {
            var animais = acAnimal.consultaAnimalPorId(id);
            ViewBag.desc = animais.descricaoAnimal;
            ViewBag.imagem = animais.imagemAnimal;
            return View(animais);
        }

        [HttpPost]
        public ActionResult DadosPet(Animal animal, HttpPostedFileBase file, FormCollection frm)
        {
            try
            {
                if (file != null)
                {
                    string arquivo = Path.GetFileName(file.FileName);
                    string file2 = "/img/" + arquivo;
                    string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                    file.SaveAs(path);
                    animal.imagemAnimal = file2;
                }

                else
                {
                    animal.descricaoAnimal = frm["descAnimal"];
                }
                acAnimal.atualizaAnimal(animal);
                TempData["success"] = "Atualização efetuada com sucesso!";
            }

            catch
            {
                TempData["error"] = "Erro ao atualizar pet";
            }
            var animalAtualizado = acAnimal.consultaAnimalPorId(animal.codAnimal);
            ViewBag.desc = animal.descricaoAnimal;
            ViewBag.imagem = animalAtualizado.imagemAnimal;
            return View(animalAtualizado);
        }

        public ActionResult ListaPet()
        {
            int cliente = Convert.ToInt32(Session["Cliente"]);
            var animais = acAnimal.consultaAnimalPorCliente(cliente);
            return View(animais);
        }

        [HttpPost]
        public JsonResult obterAnimalAgendamento(int pet)
        {
            var animal = acAnimal.consultaAnimalPorId(pet);
            return Json(animal.nomeAnimal);
        }
    }
}
