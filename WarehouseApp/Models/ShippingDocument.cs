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

    public partial class ShippingDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShippingDocument()
        {
            this.ContentGoodsShippingDocument = new HashSet<ContentGoodsShippingDocument>();
        }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Номер должен быть положительным числом")]
        public int ShippingDocumentNumber { get; set; }

        [Required]
        [Range(0, Int16.MaxValue, ErrorMessage = "Номер должен быть положительным числом")]
        public short Warehouse_WarehouseNumber { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> UnloadingDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContentGoodsShippingDocument> ContentGoodsShippingDocument { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
