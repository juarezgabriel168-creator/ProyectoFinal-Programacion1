# SISTEMAS DE RESERVA DE BUTACAS

> Proyecto final - TUP - UTN

---

 Es un progrma dirigido a la industria del cine y teatros, donde les brindamos un servicio de registracion de los tikets vendidos
 nuestro objetivos es el orden y eliminar errores como tikets duplicados, ubicacion erronea de las personas que realizaron la compra
 de una entrada, etc.

---

## Funcionalidades

---

### Pantalla de configuracion

- Tan solo se ejecuta una vez al iniciar el rpograma
- Permite definir:
 - Cantidad de filas (A - Z)
 - Cantidad de asientos por fila (Max 50)
 - Precio del ticket VIP
 - Precio del ticket NORMAL
 - Los parametros se cargan en **Configuracion.txt**

---

### Menu

| Opción | Función |
|---|---|
| 1 | Reservar asiento |
| 2 | Ver mapa de la sala |
| 3 | Reubicar espectador |
| 4 | Cancelar reserva |
| 0 | Salir |

---

### Reservar asiento

- Muestra las zonas de precios disponibes, antes de pedir los datos
- El precio y las categorias, se asignan automaticamente al seleccionar la fila
- Valida los asintos que no esten ubicados

---

### Ver mapa de sala

- Muestra las grillas con colores
  - 🟢 `[ ]` Libre
  - 🔴 `[X]` Ocupado normal
  - 🟡 `[V]` Ocupado VIP
- Muestra un resumen de ocupacion y precios vigentes

---

### Reubicar espectador

- Muestra las reservas activas
- Recalcula precio y categoria segun la nueva fila

---

### Cancelar reseva

- Muestra las reservas activas
- pide confirmacion antes de eliminar

---

### Salir

- Termina la ejecucion y muestra un cartel de despedida

---

## Estructura de proyecto

```
ReservaButacas/
│
├── Program.cs                        
├── Constantes.cs                     
│
├── Modelos/
│   ├── Butaca.cs                    
│   └── Configuracion.cs            
│
├── Servicios/
│   ├── ServicioButacas.cs          
│   ├── ArchivoButacas.cs             
│   └── ArchivoConfiguracion.cs       
│
├── Vistas/
│   ├── VistaConfiguracion.cs        
│   ├── Menu.cs                      
│   └── Vistas.cs                     
│
├── butacas.txt                     
└── configuracion.txt               
```

---

## Como ejecutar

---
 
### Requisitos

- [.NET SDK] 6.0 o superior
- Visual Studio 2022 o Visual Studio Code

---

### Pasos

1.Clonar o descargar el repositorio:

```bash
git clone https://github.com/juarezgabriel168-creator/ProyectoFinal-Programacion1.git
cd ReservaButacas
```
 
2.Abrir la solución en Visual Studio y presionar `F5`, o desde la terminal:

```bash
dotnet run
```

---

## Ejemplo de ejecucion

---

```Configuracion inicial
╔══════════════════════════════════════╗
║     CONFIGURACIÓN DEL SISTEMA        ║
║     Acceso exclusivo del propietario ║
╚══════════════════════════════════════╝

  Primera ejecución. Configure el sistema antes de continuar.

  Cantidad de filas (1-26): 4
  → La sala tendrá filas de A a D.

  Asientos por fila (1-50): 8
  → Total de asientos: 32.

  Precio ticket normal: $1500
  Precio ticket VIP:    $2500

  Las filas disponibles son A a D.
  Indique desde qué fila comienzan los asientos VIP.
  (La fila A no puede ser VIP — debe haber al menos una fila normal.)

  Fila inicio VIP (B a D): C
  → Filas normales: A a B.
  → Filas VIP:      C a D.

  ┌─────────────────────────────────────┐
  │         RESUMEN DE CONFIGURACIÓN    │
  ├─────────────────────────────────────┤
  │  Filas          : 4 (A-D)           │
  │  Asientos/fila  : 8                 │
  │  Total asientos : 32                │
  │  Precio normal  : $1500.00          │
  │  Precio VIP     : $2500.00          │
  │  Zona VIP       : C-D               │
  └─────────────────────────────────────┘

  ¿Confirmar esta configuración? (S/N): S

  ✓ Configuración guardada correctamente.

  Presione una tecla para ingresar al sistema...
  ```

---
  
```Pantalla de inicio
  ╔══════════════════════════════════════╗
  ║   SISTEMA DE RESERVA DE BUTACAS      ║
  ║   Iniciando sistema...               ║
  ╚══════════════════════════════════════╝

  ✓ Archivo butacas.txt creado. No hay reservas previas.

  Presione una tecla para continuar...
```

---

```Menu principal
╔══════════════════════════════════════╗
║   SISTEMA DE RESERVA DE BUTACAS      ║
╠══════════════════════════════════════╣
║  1. Reservar asiento                 ║
║  2. Ver mapa de la sala              ║
║  3. Reubicar espectador              ║
║  4. Cancelar reserva                 ║
║  0. Salir                            ║
╚══════════════════════════════════════╝
   Sala: 4 filas × 8 asientos  |  Normal $1500.00  VIP $2500.00
   Ingrese una opción: 1
```

---

```Opción 1 — Reservar asiento
── RESERVAR ASIENTO ──────────────────

  Zonas de precio:
  • Normal  (A-B): $1500.00
  • VIP     (C-D): $2500.00

  Ingrese fila: B
  Ingrese número de asiento: 3

  Tipo de asiento : Normal
  Precio asignado : $1500.00

  Nombre del espectador: Juan Pérez

  ✓ Reserva creada para Juan Pérez en B-3 (Normal, $1500.00).

  Presione una tecla para continuar...
```

---

```Opcion 0 — Salir
  ╔══════════════════════════════════════╗
  ║   Gracias por usar el sistema.       ║
  ║   ¡Hasta pronto!                     ║
  ╚══════════════════════════════════════╝
```

---

## Validaciones

---

### Configuración inicial

- **Cantidad de filas:** debe ser un número entero entre 1 y 26.
- **Asientos por fila:** debe ser un número entero entre 1 y 50.
- **Precio normal:** debe ser mayor a $0.01.
- **Precio VIP:** debe ser mayor a $0.01. Si es menor al precio normal, el sistema avisa con una advertencia y pide confirmación antes de continuar.
- **Fila inicio VIP:** debe ser una letra entre B y la última fila configurada. La fila A no puede ser VIP porque debe existir al menos una fila normal.

---

### Reservar asiento

- **Fila:** debe ser una letra dentro del rango configurado (de A hasta la última fila).
- **Número de asiento:** debe ser un entero entre 1 y el máximo configurado.
- **Nombre del espectador:** no puede estar vacío ni contener solo espacios.
- **Duplicado:** la combinación Fila + Número no puede estar ya ocupada. Si lo está, la operación se rechaza y se informa al usuario.

---

### Reubicar espectador

- **Asiento actual:** debe existir una reserva en esa posición. Si no existe, la operación se cancela antes de pedir el destino.
- **Nuevo asiento:** la fila y el número deben estar dentro del rango configurado.
- **Destino ocupado:** si el asiento de destino ya está reservado, la operación se rechaza.

---

### Cancelar reserva

- **Asiento:** debe existir una reserva en esa posición. Si no existe, se informa y vuelve al menú.
- **Sin reservas:** si no hay ninguna reserva registrada, el sistema lo informa y vuelve al menú automáticamente sin pedir datos.
- **Confirmación:** antes de eliminar se pide confirmación. Solo acepta S o N. Si el usuario responde N, la reserva no se elimina.

---

### Menú principal

- **Opción:** debe ser un número entero entre 0 y 4. Cualquier otro valor o letra es rechazado.

---

### Comportamiento general ante errores

- El programa nunca se cierra ni se rompe ante una entrada incorrecta.
- Ante cualquier dato inválido muestra el mensaje de error y vuelve a pedir el mismo campo.
- Los campos se validan uno por uno antes de avanzar al siguiente.
- Los precios aceptan tanto punto como coma como separador decimal (1500.50 y 1500,50 son equivalentes).
- Las letras de fila se aceptan en mayúscula y minúscula (a y A son equivalentes).Has usado 90% de tu límite de sesión

---

## Archivos

Los archivos se crean automáticamente en la carpeta del ejecutable.
 
**`configuracion.txt`**

```c
6,10,1500.00,2500.00,3
```

(cantidadFilas, asientosPorFila, precioNormal, precioVip, indiceFilaInicioVip)
 
**`butacas.txt`** — una línea por reserva:

```c
A,5,Juan Pérez,1500.00,false
D,3,María González,2500.00,true
```

(fila, numero, nombreEspectador, precio, esVip)
 
> Para reiniciar el sistema desde cero borrá ambos archivos.
 
---

## Tecnologias usadas 

| Tecnología | Uso |
|---|---|
| C# | Lenguaje de programación |
| .NET 6+ | Plataforma de ejecución |
| Archivos .txt (CSV) | Persistencia de datos |
| Git | Control de versiones |

---

## Participantes del Proyecto

- Juarez Gabriel
- Serrano Martin
- Lobo Joaquin Tomas 

![Final](https://github.com/juarezgabriel168-creator/ProyectoFinal-Programacion1/blob/main/imagen/Foto.jpg?raw=true)
