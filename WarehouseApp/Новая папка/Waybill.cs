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

    public partial class Waybill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Waybill()
        {
            this.ContentGoodsWaybill = new HashSet<ContentGoodsWaybill>();
        }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Номер должен быть положительным числом")]
        public int WaybillNumber { get; set; }

        [Required]
        [Range(0, Int16.MaxValue, ErrorMessage = "Номер должен быть положительным числом")]
        public short Warehouse_WarehouseNumber { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> EntertDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContentGoodsWaybill> ContentGoodsWaybill { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
