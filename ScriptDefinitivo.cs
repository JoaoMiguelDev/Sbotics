//Esse script em C# pertence à equipe Olimpo, com os integrantes de nome João Miguel, Pedro Leite e Filipe Adônis

// Funções de Controle
async Task SeguirFrente(double velocidade = 400)
{
    //Destrava os Motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoFrente").Locked = false; 
    Bot.GetComponent<Servomotor>("EsquerdoTras").Locked = false; 
    Bot.GetComponent<Servomotor>("DireitoTras").Locked = false;

    //Todos os Motores rodam para frente
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(Math.Abs(velocidade), velocidade/2); 
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(Math.Abs(velocidade), velocidade/2); 
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(Math.Abs(velocidade), velocidade/2);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(Math.Abs(velocidade), velocidade/2); 
}

async Task VirarDireita(double velocidade = 200)
{
    //Destrava os Motores
    Bot.GetComponent<Servomotor>("DireitoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoTras").Locked = false;
    Bot.GetComponent<Servomotor>("EsquerdoTras").Locked = false;

    //Motores da Esquerda rodam para frente
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(Math.Abs(velocidade * 2), velocidade * 2);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(Math.Abs(velocidade * 2), velocidade * 2);

    //Motores da Direita rodam para trás
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(Math.Abs((0 - velocidade) * 2), (0 - velocidade) * 2);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(Math.Abs((0 - velocidade) * 2), (0 - velocidade) * 2);
}

async Task VirarEsquerda(double velocidade = 200)
{
    //Destrava os Motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("EsquerdoTras").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoTras").Locked = false;

    //Motores da Direita rodam para frente
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(Math.Abs(velocidade * 2), velocidade * 2);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(Math.Abs(velocidade * 2), velocidade * 2);

    //Motores da Esquerda rodam para trás
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(Math.Abs((0 - velocidade) * 2), (0 - velocidade) * 2);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(Math.Abs((0 - velocidade) * 2), (0 - velocidade) * 2);
}

async Task ParaTras(double velocidade = 200)
{
    //Destrava os Motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("EsquerdoTras").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoTras").Locked = false;

    //Todos os Motores rodam para trás
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
}

async Task Girar180(double velocidade = 150)
{
    //Guarda a cor captada pelo sensor do meio
    var SensorMeio = Bot.GetComponent<ColorSensor>("SensorMeio");

    //Destrava os Motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoFrente").Locked = false;
    Bot.GetComponent<Servomotor>("EsquerdoTras").Locked = false;
    Bot.GetComponent<Servomotor>("DireitoTras").Locked = false;

    // Começa a girar no próprio eixo
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(Math.Abs(0 - velocidade), 0 - velocidade);
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(Math.Abs(velocidade), velocidade);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(Math.Abs(velocidade), velocidade);

    // Faz o robô sair do preto
    while (SensorMeio.Analog.ToString() == "Preto")
    {
        await Time.Delay(1);
    }

    // Faz o robô ver o preto (girou 180°)
    while (SensorMeio.Analog.ToString() != "Preto")
    {
        await Time.Delay(1);
    }

    // Para os motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(0, 0);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(0, 0);
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(0, 0);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(0, 0);
}

async Task Parar(double velocidade = 0)
{
    //Para todos os motores
    Bot.GetComponent<Servomotor>("EsquerdoFrente").Apply(0, 0);
    Bot.GetComponent<Servomotor>("EsquerdoTras").Apply(0, 0);
    Bot.GetComponent<Servomotor>("DireitoFrente").Apply(0, 0);
    Bot.GetComponent<Servomotor>("DireitoTras").Apply(0, 0);
}

async Task Main()
{
    while (true)
    {
        await Time.Delay(1); // NECESSÁRIO PARA A APLICAÇÃO NÃO DAR CRASH

        //Variáveis que armazenam as cores captadas pelos sensores
        var SensorDireito = Bot.GetComponent<ColorSensor>("SensorDireito").Analog.ToString();
        var SensorEsquerdo = Bot.GetComponent<ColorSensor>("SensorEsquerdo").Analog.ToString();
        var SensorDireitoFora = Bot.GetComponent<ColorSensor>("SensorDireitoFora").Analog.ToString();
        var SensorEsquerdoFora = Bot.GetComponent<ColorSensor>("SensorEsquerdoFora").Analog.ToString();
        var SensorMeio = Bot.GetComponent<ColorSensor>("SensorMeio").Analog.ToString();

        //Variáveis que armazenam os parâmetros RGB do sensor do meio (para implementar a função sala de resgate)
        double azul = Bot.GetComponent<ColorSensor>("SensorMeio").Analog.Blue;
        double verde = Bot.GetComponent<ColorSensor>("SensorMeio").Analog.Green;
        double vermelho = Bot.GetComponent<ColorSensor>("SensorMeio").Analog.Red;

        //Variáveis que analisam se o sensor de toque foi ativado (para que seja possível desviar dos obstáculos)
        bool ToqueEsquerdo = Bot.GetComponent<TouchSensor>("ToqueEsquerdo").Digital;
        bool ToqueDireito = Bot.GetComponent<TouchSensor>("ToqueDireito").Digital;

        if (Convert.ToInt32(vermelho) == 77 && Convert.ToInt32(azul) == 96 && Convert.ToInt32(verde) == 85) //Vê cinza, ignora a sala de resgate 
        {
            await SeguirFrente(500);
            await Time.Delay(1500);
            await VirarDireita(500);
            await Time.Delay(825);
        }

        else if (SensorDireito == "Preto" && SensorEsquerdo == "Preto") //Preto dos dois lados, segue em frente
        {
            await SeguirFrente(400);
        }

        else if (SensorDireito == "Verde" && SensorEsquerdo == "Verde") //Verde nos dois lados, gira 360°
        {
            await ParaTras(300);
            await Girar180(150);
            await Time.Delay(500);
        }

        else if (SensorDireito == "Verde" || SensorDireitoFora == "Verde") //Verde na direita, para e em seguida vira à direita
        {
            await Parar();
            await Time.Delay(400);

            await VirarDireita(600);
            await Time.Delay(500);

            await SeguirFrente(600);
            await Time.Delay(400);
        }

        else if (SensorEsquerdo == "Verde" || SensorEsquerdoFora == "Verde") //Verde na esquerda, para e em seguida vira à esquerda
        {
            await Parar();
            await Time.Delay(400);

            await SeguirFrente(500);
            await Time.Delay(500);
        }

        else if (SensorDireito == "Preto" || SensorDireitoFora == "Preto") //Preto na direita, vire à direita
        {
            await VirarDireita(400);

        }

        else if (SensorEsquerdo == "Preto" || SensorEsquerdoFora == "Preto") //Preto na esquerda, vire à esquerda
        {
            await VirarEsquerda(400);

        }

        else if (SensorDireito == "Vermelho" || SensorEsquerdo == "Vermelho") //Vermelho em qualquer um dos lados, venceu
        {
            await Parar(0);

        }

        else if (ToqueDireito || ToqueEsquerdo) //Se um dos lados é pressionado, desvia do obstáculo
        {
            await ParaTras(500);
            await Time.Delay(400);

            await VirarDireita(500);
            await Time.Delay(1000);

            await SeguirFrente(500);
            await Time.Delay(1500);

            await VirarEsquerda(500);
            await Time.Delay(700);

            await SeguirFrente(500);
            await Time.Delay(3000);

            await VirarEsquerda(500);
            await Time.Delay(700);

            await SeguirFrente(500);
            await Time.Delay(2000);

            await VirarDireita(500);
            await Time.Delay(700);
        }

        else //Se nenhuma das condições é cumprida, segue em frente
        {
            await SeguirFrente(300);
        }
    }
}

