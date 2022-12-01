# EscolaAPI

Uma API simples com relacionamento many to many entre Alunos e Turmas

Requisitos:

Entity Framework: dotnet add package Microsoft.EntityFrameworkCore.Design
Entity Framework SQL Server: dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Regras:

1- Aluno não pode ser cadastrado repetido (validação pelo CPF);

2- No momento de cadastrar um aluno, deve-se informar pelo menos uma turma que ele irá cursar;

3- O mesmo aluno pode ser matriculado em várias turmas diferentes, porém a Matrícula não pode ser repetida na mesma turma;

4- Uma turma não pode ter mais de 5 alunos;

5- Turma não pode ser excluída se possuir alunos;
