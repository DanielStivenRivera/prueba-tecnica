const fs = require('fs');
const dotenv = require('dotenv');

dotenv.config();


const targetPath = './src/environments/environment.ts';


const envConfigFile = `
  export const environment = {
    production: false,
    apiUrl: '${process.env.API_URL}',
  };
`;


fs.writeFileSync(targetPath, envConfigFile, { encoding: 'utf8' });
console.log(`Environment variables written to ${targetPath}`);
