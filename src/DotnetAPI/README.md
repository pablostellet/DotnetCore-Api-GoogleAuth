# base-template

Repositório exemplo usado para agilizar na criação de novos projetos de Processos e Ferramentas.

## Guia de contribuição

- Cada template será uma branch deste repositório
- O nome da branch será composto pelo [backend]-[frontend]-[frameworks], sendo o último framework o responsável pelo CSS ex:

```bash
dotnetcore2.2-vue-nuxt-bootstrap
```
- Cada branch deverá conter um README.md explicando como rodar o template
- É interessante colocar links com referências para que pessoas do time possam ter o mesmo entendimento, abaixo seguem exemplos.


## Setup
precisamos ter configurados na nossa máquina:
- [npm](https://www.npmjs.com/get-npm)
- [vue-cli](https://cli.vuejs.org/guide/installation.html)
- [dotnet core 2.0+](https://dotnet.microsoft.com/download)
- [laravel](https://laravel.com/docs/4.2/installation)

```
cd <root>/
npm install
dotnet run
```

## Testes

```
cd <root>/
npm install
npm run test
```


## Backend

- dotnet core 2.2
- laravel

## FrontEnd

- vue
- nuxt
- Bootstrap 4

## Banco de Dados

Devemos definir aqui que banco usamos e se há alguma configuração necessária (ex: driver do Oracle).

## Deploy

Aqui deveremos especificar informações adicionais de como realizar deploy na nossa aplicação.

## Referências
- [vue](https://vuejs.org/)
- [vue-awesome](https://github.com/vuejs/awesome-vue): Bibliotecas testadas e recomendadas pela comunidade.
- [nuxt](https://nuxtjs.org/): Framework para vue e typescript
- [Bootstrap 4](https://getbootstrap.com.br/docs/4.1/getting-started/introduction/)
