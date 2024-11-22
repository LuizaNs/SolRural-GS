# Administração de fazendas

## Integrantes 
- Luiza Nunes de Jesus 
- Melissa de Oliveira Pecoraro 
- Pamella Schimalesky Engholm 
- Pedro Marques Pais Pavão 
- Roberto Menezes dos Santos

## Tecnologias

- [.NET 8.0](https://dotnet.microsoft.com/pt-br/)
- [Mongo DB](https://www.mongodb.com/pt-br)
- [Swagger](https://swagger.io/)

## Ferramenta

- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/)

## Getting Started

Primeiro clone o projeto em sua máquina: 
```
git clone https://github.com/LuizaNs/SolRural-GS.git
```
Após isso, abra a pasta em que o projeto está salvo na sua IDE de preferência. A utilizada para o desenvolvimento foi o Visual Studio 2022. 

# Documentação da API

Para a documentação dos endpoints encontrados na API, o exemplo utilizado será o Cultivo, isso porque todas as entidades apresentam os mesmos endpoints com as mesmas funcionalidades. 

## Cultivo

`[HttpGet]`

### Retorna todos os Usuários cadastrados
| Código | Descrição                             |
|--------|---------------------------------------|
| 200    | Retorna todos os cultivos registrados |


`[HttpGet("{id}")]`

### Retorna o Cultivo pelo seu ID
| Código | Descrição                                           |
|--------|-----------------------------------------------------|
| 200    | Retorna o cultivo com o id enviado                  |
| 404    | Caso o cultivo com o id enviado não seja encontrado |

`[HttpPost]`

### Cria um Cultivo
| Código | Descrição                                           |
|--------|-----------------------------------------------------|
| 201    | Retorna o cultivo recém-criado                      |

`[HttpPut("{id}")]`

### Atualiza um Cultivo pelo ID
| Código | Descrição                                           |
|--------|-----------------------------------------------------|
| 200    | Retorna caso o cultivo seja atualizado com sucesso  |
| 404    | Caso o cultivo com o id enviado não seja encontrado |

`[HttpDelete("{id}")]`

### Deleta um Cultivo pelo ID
| Código | Descrição                                           |
|--------|-----------------------------------------------------|
| 204    | Retorna caso o cultivo seja deletado com sucesso    |
| 404    | Caso o cultivo com o id enviado não seja encontrado |

# Testes

Para verificar se todos os endpoints estão funcionando corretamente, foram aplicados testes unitários utilizando Moq, que verificam cenários de erro e de sucesso. 

### Sucesso 
Retorna o cultivo se ele existir
```
public async Task GetCultivo_ReturnsCultivo_WhenCultivoExists()
```

### Falha
Caso o cultivo a ser atualizado não exista, retorna Not Found
```
public async Task UpdateCultivo_ReturnsNotFound_WhenCultivoDoesNotExist()
```

# Clean Code

No projeto Sol Rural foram utilizadas todas as práticas para um código limpo. Para facilitar a manutenabilidade e leitura do código, ele foi separado por biblioteca de classes, onde cada uma exerce uma função: Models - modelo de dados, Data - configuração de conexão com o banco, Service - lógica do acesso aos dados, Controller - expõe os endpoints da API, Factorie - implementa uma fábrica de modelo para a recomendação. 
As classes e métodos possuem nomes descritivos que refletem claramente suas funcionalidades, facilitando o entendimento do código. Além disso, as operações de banco de dados são realizadas de forma assíncrona, o que aumenta a eficiência e melhora a capacidade de resposta da aplicação, especialmente em situações com grande volume de requisições

# IA Generativa

No projeto Sol Rural a IA foi aplicada de forma que a recomendação da instalação seja inteligente. Para diminuir os gastos com a instalação, a IA verifica o tipo de cultivo, e através dele ela recomenda qual seria a melhor instlação para a fazenda. 
