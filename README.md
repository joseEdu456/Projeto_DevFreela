# 📌 DevFreela

**DevFreela** é uma aplicação desenvolvida com foco na criação e gerenciamento de projetos para freelancers. Nela, é possível cadastrar usuários, criar projetos, associar habilidades a cada usuário e gerenciar o fluxo de trabalho de forma eficiente.

Este projeto foi desenvolvido com o objetivo de praticar e aplicar conceitos essenciais de desenvolvimento de APIs utilizando o padrão REST, banco de dados relacional (**SQL Server**) e o ORM **Entity Framework Core**.

## 🔧 Tecnologias e conceitos aplicados

- ASP.NET Core com Web API
- SQL Server
- Entity Framework Core
- Autenticação com JWT
- Padrão Repository
- Injeção de Dependência
- Arquitetura Limpa (Clean Architecture)
- Criação de testes automatizados
- Boas práticas de organização de código e separação de responsabilidades

---
## 🚀 Instalação e Uso

- É necessário ter o [.NET 8.0 instalado](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- É necessário o uso do SQL Server para o banco de dados
- Recomendo o uso do Visual Studio para rodar a aplicação

### 🔄 Como rodar a aplicação

Primeiro clone o repositório do github
```bash
git clone https://github.com/joseEdu456/Projeto_DevFreela.git
```
No arquivo appsettings.json, na seção de conexão com o banco altere a conection string ProjectConnectionString para a seguinte:

```bash
"Data source=SeuLocalHost; Initial Catalog=DevFreelaProj; Integrated Security=true; TrustServerCertificate=True"
```
Após isso abra o NugetPackage Console e rode os seguinte comando para aplicar as migrations no banco de dados
```bash
Update-Database -Context DevFreelaDbContext
```
Por fim, basta iniciar a aplicação que então o Swagger será aberto automaticante, sendo possivel visualizar todos os endpoint da API

---
## 👨‍💻 Sobre o Autor

Sou estudante do **7º período de Ciência da Computação**, apaixonado por tecnologia, desenvolvimento de software e boas práticas de programação. Este projeto faz parte da minha jornada de aprendizado, com foco especial em APIs, arquitetura de sistemas e desenvolvimento backend com C# e .NET.

Sempre buscando evoluir, gosto de transformar teoria em prática através de projetos que reforcem conceitos fundamentais e preparem para desafios do mundo real.

🔗 [Acesse meu LinkedIn](https://www.linkedin.com/in/josé-eduardo-rubio-da-silva-brianez-39746b246/)
