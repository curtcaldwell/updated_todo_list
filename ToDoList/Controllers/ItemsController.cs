using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class ItemController : Controller
  {
    [HttpPost("/items")]
    public ActionResult CollectInfo(string newitem)
    {
      Item newItem = new Item(newitem);
      newItem.Save();
      // List<Item> all = Item.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/items")]
    public ActionResult Index()
    {
      // List<Item> all = Item.GetAll();
      return View(Item.GetAll());
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
      return View(Category.GetAll());
    }

    [HttpGet("/items/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Item thisItem = Item.Find(id);
      return View(thisItem);
    }

    [HttpPost("/items/{id}/update")]
    public ActionResult Update(int id)
    {
      Item thisItem = Item.Find(id);
      thisItem.Edit(Request.Form["newdescription"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/items/{id}/delete")]
      public ActionResult Delete(int id)
    {
      Item thisItem = Item.Find(id);
      return View(thisItem);
    }

    [HttpPost("/items/{id}/delete")]
    public ActionResult DeleteItem(int id)
    {
      Item thisItem = Item.Find(id);
      thisItem.Delete();
      return RedirectToAction("Index");
    }
    [HttpGet("/items/{id}")]
       public ActionResult Details(int id)
       {
           Dictionary<string, object> model = new Dictionary<string, object>();
           Item selectedItem = Item.Find(id);
           List<Category> itemCategories = selectedItem.GetCategories();
           List<Category> allCategories = Category.GetAll();
           model.Add("selectedItem", selectedItem);
           model.Add("itemCategories", itemCategories);
           model.Add("allCategories", allCategories);
           return View(model);

       }
       [HttpPost("/items/{itemId}/categories/new")]
       public ActionResult AddCategory(int itemId)
       {
           Item item = Item.Find(itemId);
           Category category = Category.Find(int.Parse(Request.Form["new-category"]));
           item.AddCategory(category);
           return RedirectToAction("Details",  new { id = itemId });
       }



      // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {ß
    //   Item.ClearAll();
    //   return View();
    // }
  }
}
