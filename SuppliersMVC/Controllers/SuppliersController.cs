using SuppliersMVC.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SuppliersMVC.Controllers
{
    public class SuppliersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var groups = db.Groups
                .Select(s => new CheckboxViewModel
                {
                    Id = s.GroupId,
                    NameOfGroup = s.Name,
                    Checked = db.SupplierToGroup.Any(
                        w => w.SupplierId == id && w.GroupId == s.GroupId)
                }).ToList();

            var supplierViewModel = new SuppliersViewModel
            {
                SupplierId = id.Value,
                Name = supplier.Name,
                Address = supplier.Address,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber,
                Groups = groups
            };
            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            var groups = db.Groups
                .Select(s => new CheckboxViewModel
                {
                    Id = s.GroupId,
                    NameOfGroup = s.Name,
                    Checked = false
                }).ToList();

            var supplierViewModel = new SuppliersViewModel
            {
                Groups = groups
            };

            return View(supplierViewModel);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,Name,Address,Email,PhoneNumber,Groups")] SuppliersViewModel supplierWithGroups)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    SupplierId = supplierWithGroups.SupplierId,
                    Name = supplierWithGroups.Name,
                    Address = supplierWithGroups.Address,
                    Email = supplierWithGroups.Email,
                    PhoneNumber = supplierWithGroups.PhoneNumber
                };

                foreach (var group in supplierWithGroups.Groups)
                {
                    if (group.Checked)
                    {
                        db.SupplierToGroup.Add(new SupplierToGroup
                        {
                            SupplierId = supplierWithGroups.SupplierId,
                            GroupId = group.Id
                        });
                    }
                }

                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            var groups = db.Groups
                .Select(s => new CheckboxViewModel
                {
                    Id = s.GroupId,
                    NameOfGroup = s.Name,
                    Checked = db.SupplierToGroup.Any(w => w.SupplierId == id && w.GroupId == s.GroupId)
                }).ToList();

            var supplierViewModel = new SuppliersViewModel
            {
                SupplierId = id.Value,
                Name = supplier.Name,
                Address = supplier.Address,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber,
                Groups = groups
            };
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SuppliersViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                var editedSupplier = db.Suppliers.Find(supplier.SupplierId);

                if (editedSupplier != null)
                {
                    editedSupplier.Name = supplier.Name;
                    editedSupplier.Address = supplier.Address;
                    editedSupplier.Email = supplier.Email;
                    editedSupplier.PhoneNumber = supplier.PhoneNumber;
                }

                foreach (var entity in db.SupplierToGroup)
                {
                    if (entity.SupplierId == supplier.SupplierId)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                    }
                }

                if (supplier.Groups != null)
                {
                    foreach (var group in supplier.Groups)
                    {
                        if (group.Checked)
                        {
                            db.SupplierToGroup.Add(new SupplierToGroup
                            {
                                SupplierId = supplier.SupplierId,
                                GroupId = group.Id
                            });
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier != null) db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}