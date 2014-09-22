using System.Collections;
using System.Collections.Generic;

namespace NHibernateSpike.Entities
{
    public class Store
    {
        private IList<Product> _products;
        private IList<Employee> _employees;

        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }

        public virtual IList<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        public virtual IList<Employee> Staff
        {
            get { return _employees; }
            set { _employees = value; }
        }

        public Store()
        {
            _products = new List<Product>();
            _employees = new List<Employee>();
        }

        public virtual void AddProduct(Product product)
        {
            product.StoresStockedIn.Add(this);
            Products.Add(product);
        }

        public virtual void AddEmployee(Employee employee)
        {
            employee.Store = this;
            Staff.Add(employee);
        }
    }
}