using Backend_dotnet8.Core.Dtos.Cajas.Entrada;
using Backend_dotnet8.Core.Dtos.CajasRegistros.Entrada;
using Backend_dotnet8.Core.Dtos.CategoriaProductos.Entrada;
using Backend_dotnet8.Core.Dtos.Facturas.Entrada;
using Backend_dotnet8.Core.Dtos.Facturas.Salida;
using Backend_dotnet8.Core.Dtos.Negocios.Entrada;
using Backend_dotnet8.Core.Dtos.Productos.Entrada;
using Backend_dotnet8.Core.Dtos.Transacciones.Entrada;
using Backend_dotnet8.Core.Dtos.Transacciones.Salida;
using Backend_dotnet8.Core.Entities;
using System.Drawing;


namespace Backend_dotnet8.Controllers
{
    public class Mapping
    {
        public static Negocio GetMapper(NegocioEntrada valor)
        {
            return new Negocio()
            {
                Barrio = valor.Barrio,
                Direccion = valor.Direccion,
                EstaRegistradoCamaraComercio = valor.EstaRegistradoCamaraComercio,
                Logo = valor.Logo,
                Nit = valor.Nit,
                TieneLocalFisico = valor.TieneLocalFisico,
                Nombre = valor.Nombre,
                IdPropietario = valor.IdPropietario

            };
        }
        public static Caja GetMapper(CajaEntrada valor)
        {
            return new Caja ()
            {
                NumeroCaja = valor.NumeroCaja,
                IdNegocio = valor.IdNegocio

            };
        }
        public static CajaRegistro GetMapper(CajaRegistroEntrada valor)
        {
            return new CajaRegistro()
            {
                DineroApertura = valor.DineroApertura,
                DiferenciaCierre = valor.DiferenciaCierre,
                DineroCierre = valor.DineroCierre,
                Ganacias = valor.Ganacias,
                TotalFacturasDiarias = valor.TotalFacturasDiarias,
                IdCaja = valor.IdCaja,

            };
        }
        public static CategoriaProducto GetMapper(CategoriaProductoEntrada valor)
        {
            return new CategoriaProducto()
            {
                Descripcion = valor.Descripcion,

            };
        }
        public static Factura GetMapper(FacturaEntrada valor)
        {
            return new Factura()
            {
                NumeroFactura = valor.NumeroFactura,
                DineroRecibido = valor.DineroRecibido,
                TotalMonto = valor.TotalMonto,
                Cambio = valor.Cambio,
                Items = valor.Items,
                FechaExpedicion = valor.FechaExpedicion,
                FechaImpresion = valor.FechaImpresion,
                FormaPago = valor.FormaPago,
                IdCajaRegistro = valor.IdCajaRegistro,
                IdCajero = valor.IdCajero
                

            };
        }
        public static Producto GetMapper(ProductoEntrada valor)
        {
            return new Producto()
            {
                Codigo = valor.Codigo,
                Nombre = valor.Nombre,
                PrecioCompra = valor.PrecioCompra,  
                PrecioVenta = valor.PrecioVenta,    
                Stock = valor.Stock,
                IdCategoriaProducto = valor.IdCategoriaProducto

            };
        }
        public static Transaccion GetMapper(TransaccionEntrada valor)
        {
            return new Transaccion()
            {
                Cantidad = valor.Cantidad,
                TotalMonto = valor.TotalMonto,
                IdProducto = valor.IdProducto,
                IdFactura = valor.IdFactura
                
            };
        }
        public static TransaccionSalida GetMapper(Transaccion valor)
        {
            return new TransaccionSalida()
            {
                Cantidad = valor.Cantidad,
                TotalMonto= valor.TotalMonto,
                IdFactura= valor.IdFactura,
                IdProducto = valor.IdProducto
            };
        }
        public static List<TransaccionSalida> GetMapper(List<Transaccion> transacciones)
        {

            List<TransaccionSalida> transaccionSalidas = new List<TransaccionSalida>();

            foreach (var transaccion in transacciones)
            {
                transaccionSalidas.Add(GetMapper(transaccion));
            }

            return transaccionSalidas;
        }
        public static FacturaInfoSalida GetMapper(Factura factura,List<Transaccion> transacciones)
        {
            return new FacturaInfoSalida()
            {
                
                NumeroFactura = factura.NumeroFactura,
                DineroRecibido = factura.DineroRecibido,
                Items = factura.Items,
                Cambio = factura.Cambio,
                FechaExpedicion = factura.FechaExpedicion,
                FechaImpresion = factura.FechaImpresion,
                FormaPago = factura.FormaPago,
                TotalMonto = factura.TotalMonto,
                IdCajaRegistro = factura.IdCajaRegistro,
                IdCajero = factura.IdCajero,
                transacciones = GetMapper(transacciones),

            };
        }
    }
}
