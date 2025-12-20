# Universal Client Base (UCB) - .NET Edition
Un proyecto plantilla (boilerplate) minimalista, listo para producción y auto-desplegable, construido con **C#/.NET 8** y **React**. Perfecto como punto de partida para un CRM, una app interna o cualquier MVP.
[![Licencia: MIT](https://img.shields.io/badge/Licencia-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

---

## 🚀 Quick Start 
El único requisito es tener **Docker** y **Docker Compose** instalados.
1.  Clona el repositorio:
  ```bash
    git clone [https://github.com/arzep24/ucb-dotnet.git](https://github.com/arzep24/ucb-dotnet.git)
    cd ucb-dotnet
    ```
2.  Levanta los contenedores:
```bash
    docker-compose up -d --build
```
3.  ¡Listo!
    * **Frontend:** `http://localhost:3000`
    * **Backend API:** `http://localhost:8080`
    * **Swagger Docs:** `http://localhost:8080/swagger`

---
## 🏛️ Arquitectura 
### Backend (.NET 8) Sigue los principios de **Clean Architecture**: 
* `Core`: Entidades de dominio. 
* `Application`: Casos de uso. 
* `Infrastructure`: EF Core y bases de datos. 
* `API`: Controladores REST. 
### Frontend (Vue 3) 
Utiliza el patrón **Composition API** con `<script setup>` para un código más limpio y modular. 
* **Pinia:** Para el manejo de estado global (Store). 
* **Vue Router:** Para la navegación SPA (Single Page Application). 
* **Vite:** Para un servidor de desarrollo ultrarrápido.
---
## 🏛️ Estructura del Proyecto
Este proyecto utiliza Clean Architecture para asegurar la separación de responsabilidades y la mantenibilidad.
```
/src 
├── Core/ # Entidades y lógica de negocio pura. No depende de nada. 
├── Application/ # Lógica de aplicación, DTOs y casos de uso. 
├── Infrastructure/ # Implementaciones: Repositorios, EF Core DbContext. 
└── API/ # Punto de entrada: Controladores/Minimal APIs, Swagger.
```

## 🛠️ Comandos Útiles **Crear una migración de BD (desde `src/API`):** 
```bash 
dotnet ef migrations add NombreMigracion -p ../Infrastructure/ -s .
```

## 📈 Roadmap (Mejoras Futuras)
* [ ] Autenticación y Autorización (JWT).
* [ ] Borrado Lógico (Soft-delete) en lugar de físico.
* [ ] Pruebas unitarias y de integración.
* [ ] CI/CD Básico con GitHub Actions.