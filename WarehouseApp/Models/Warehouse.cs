//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WarehouseApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Warehouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Warehouse()
        {
            this.ShippingDocument = new HashSet<ShippingDocument>();
            this.Waybill = new HashSet<Waybill>();
        }

        [Required]
        [Range(0, Int16.MaxValue, ErrorMessage = "Номер должен быть положительным числом")]
        public short WarehouseNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} длина должна быть между {2} и {1}.", MinimumLength = 5)]
        public string Storekeeper { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingDocument> ShippingDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Waybill> Waybill { get; set; }
    }
}
