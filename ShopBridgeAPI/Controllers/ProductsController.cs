using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShopBridgeAPI.Models;
using TransactionLogger;


namespace ShopBridgeAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductDb db = new ProductDb();

        FileLogger fileLogger = new FileLogger();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            try
            {
                return db.Products;
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this, Ex.StackTrace);
                return null;
            }
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {

                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this, Ex.StackTrace);
                return NotFound();
            }
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != product.ID)
                {
                    return BadRequest();
                }

                db.Entry(product).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this, Ex.StackTrace);
                return BadRequest("Exception");
            }
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Products.Add(product);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this, Ex.StackTrace);
                return BadRequest("Exception");
            }
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

             
                db.Products.Remove(product);
                db.SaveChanges();

                return Ok(product);
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this, Ex.StackTrace);
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            try
            {
                return db.Products.Count(e => e.ID == id) > 0;
            }
            catch (Exception Ex)
            {
                fileLogger.WriteError(this,Ex.StackTrace);
                return false;
            }
        }
       
    }
}