using BornToMove.ASPNET.Models;
using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BornToMove.ASPNET.Controllers
{
    public class MovesController : Controller
    {
        private BuMove _buMove;
        public MovesController(BuMove buMove)
        {
            _buMove = buMove;
        }
        public IActionResult Index()
        {
            List<MoveRating> moves = _buMove.GetAllMoves();// code om de lijst met moves op te halen inclusief de gemiddelde rating
            ViewBag.Moves = moves;
            return View(moves);
        }
        public IActionResult Details(int id)
        {
            MoveRating? move = _buMove.GetMoveById(id); // code om een enkele move op te halen met dit id inclusief de gemiddelde rating
            return View(move);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details([FromForm] MoveRating m) {
            if (ModelState.IsValid) {
                m.Move = _buMove.GetMoveById(m.Move.id)?.Move ?? m.Move;
                _buMove.AddRating(m);
                return RedirectToAction(nameof(Index));
            }
            m = _buMove.GetMoveById(m.Move.id) ?? m;
            return View(m);
        }

        public IActionResult Edit(int id) {
            Move? move = _buMove.GetMoveById(id)?.Move;
            return View(move);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[FromForm] Move updatedMove) {
            if (updatedMove.id != id) {
                ModelState.AddModelError("id", "Bad Request");
            }
            bool other = _buMove.doesMoveExist(updatedMove.name) && _buMove.GetMoveByName(updatedMove.name)?.id != id;
            if (other) {
                ModelState.AddModelError("name", "This move name is already being used.");
            }
            if (ModelState.IsValid) {
                _buMove.EditMove(updatedMove);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedMove);
        }

        public IActionResult Delete(int id) {
            Move? move = _buMove.GetMoveById(id)?.Move;
            return View(move);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [FromForm] Move m) {
            if (m.id != id) {
                ModelState.AddModelError("id", "Bad Request");
                return View(m);
            }
            _buMove.DeleteMove(m);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Random()
        {
            MoveRating randomMove = _buMove.GetRandomMove(); // code om een random move op te halen
            return View("Details", randomMove);
        }

        public IActionResult CreateMove() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMove(Move m) {
            if(_buMove.doesMoveExist(m.name)) {
                ModelState.AddModelError("name", "This move name is already being used.");
            }
            if (ModelState.IsValid) {
                _buMove.AddMove(m);
                return RedirectToAction(nameof(Index));
            }
            return View(m);
        }
    }
}
