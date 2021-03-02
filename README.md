# Sales Dev
Sales Dev é uma API que realiza o CRUD de Clientes, Produtos e Vendas.

## Como rodar:

Clone o repositório e execute pelo Visual Studio.

Ou se preferir, execute o comando a seguir no Postman:

```bash
https://salesdev-api.azurewebsites.net/api/clientes
https://salesdev-api.azurewebsites.net/api/produtos
https://salesdev-api.azurewebsites.net/api/vendas
```
Para Adicionar(CREATE) e Atualizar(UPDATE) um Cliente usa-se o seguinte corpo:

```bash
{
  "nomeCompleto": "string",
  "dataNascimento": "2021-03-02T18:51:39.582Z",
  "email": "string",
  "dataCadastro": "2021-03-02T18:51:39.583Z",
  "ativo": true
}
```

Para Adicionar(CREATE) e Atualizar(UPDATE) um Produto usa-se o seguinte corpo:

```bash
{
  {
  "descricao": "string",
  "preco": 0,
  "ativo": true
  }
}
```

Para Adicionar(CREATE) e Atualizar(UPDATE) uma Venda usa-se o seguinte corpo:

```bash
{
  {
  "quantidade": 0,
  "dataVenda": "2021-03-02T19:02:45.061Z",
  "idCliente": 0,
  "idProduto": 0
}
}
```

```bash
https://salesdev-api.azurewebsites.net/api/clientes/1
https://salesdev-api.azurewebsites.net/api/produtos/1
https://salesdev-api.azurewebsites.net/api/vendas/1
```
Para Atualizar(UPDATE), Obter um ou todos(READ) e Cancelar(DELETE).



OBS: Vendas só possui o método Adicionar e Obter um.
