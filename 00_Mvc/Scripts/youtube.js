const opciones = {
    method: 'GET'
}
//https://unsplash.com/oauth/applications/339888
//https://unsplash.com/oauth/authorize?client_id=PARENT_APPLICATION_CLIENT_ID&redirect_uri=https://mywordpressinstall.com/unsplash_callback&response_type=code&scope=public
function miAjax(param) {
    var miTabla = "";
    var miTabla01 =
        "   <table>" +
        "        <thead>" +
        "           <tr>" +
        "               <th>title</th>" +
        "               <th>url</th>" +
        "               <th>image path</th>" +
        "               <th>image</th>" +
        "               <th>color</th>" +
        "               <th>videoId</th>" +
        "           </tr>" +
        "        </thead>" +
        "        <tbody>";
    var miTabla02 = "";
    var miTabla03 =
        "       </tbody>" +
        "   </table>";
    var contador = 0;
    fetch('https://www.googleapis.com/youtube/v3/search?key=AIzaSyAbYQeSBSSwsawljAP4liZSU9GbLLQW5-c&channelId=UCcyeS-u6ErEO-Az7J56DsIg&part=snippet,id&order=date&maxResults=5', opciones)
        .then(respuesta => respuesta.json())
        .then(resultado => {
            resultado.items.forEach(elemento => {
                if (param != null && param == contador) {
                    miTabla02 = miTabla02 +
                        "                    <tr style='background-color:" + elemento.kind + ";'>" +
                        "                        <td>" + elemento.etag + "</td>" +
                        "                        <td>" + elemento.nextPageToken + "</td>" +
                        "                        <td>" + elemento.regionCode + "</td>" +
                        "                        <td>" + elemento.pageInfo + "</td>" +
                        "                        <td>" + "<a href='" + elemento.videoId + "'></a>" + "</td>" +
                        "                    </tr>";
                }
                if (param == null) {
                    miTabla02 = miTabla02 +
                        "                    <td>" + elemento.kind + "</td>" +
                        "                        <td>" + elemento.etag + "</td>" +
                        "                        <td>" + elemento.nextPageToken + "</td>" +
                        "                        <td>" + elemento.regionCode + "</td>" +
                        "                        <td>" + elemento.pageInfo + "</td>" +
                        "                        <td>" + "<a href='" + videoId + "'></a>" + "</td>" +
                        "                    </tr>";
                }
                contador = contador + 1;
            });
            miTabla = miTabla01 + miTabla02 + miTabla03;
            document.getElementById("miTabla").innerHTML = miTabla;
        });

}
function cambiaNumero() {
    var numero = parseInt(document.getElementById("text").value);
    miAjax(numero);
    numero = numero + 1;
    if (numero > 10) {
        numero = 1;
    }
    document.getElementById("text").value = numero;
}
function miOtroAjax(param) {

    var contador = 0;
    //https://www.googleapis.com/youtube/v3/search?key=AIzaSyAbYQeSBSSwsawljAP4liZSU9GbLLQW5-c&channelId=UCcyeS-u6ErEO-Az7J56DsIg&part=snippet,id&order=date&maxResults=5
    fetch(' https://www.googleapis.com/youtube/v3/search?key=AIzaSyAbYQeSBSSwsawljAP4liZSU9GbLLQW5-c&channelId=UCcyeS-u6ErEO-Az7J56DsIg&part=snippet,id&order=date&maxResults=5', opciones)
        .then(respuesta => respuesta.json())
        .then(resultado => {
            resultado.items.forEach(elemento => {
                if (param != null && param == contador) {
                    document.getElementById("p01").innerHTML = elemento.kind;
                    document.getElementById("p02").innerHTML = elemento.etag;
                    document.getElementById("p03").innerHTML = elemento.nextPageToken;
                    document.getElementById("p04").innerHTML = elemento.regionCode;
                    document.getElementById("p05").innerHTML = elemento.pageInfo;
                    document.getElementById("miVideo").src = "https://www.youtube.com/embed/" + "SZA42zDZqI0";//elemento.videoId;             
                }
                //// https://youtu.be/SZA42zDZqI0
                //// key: AIzaSyAbYQeSBSSwsawljAP4liZSU9GbLLQW5-c
                //// https://www.youtube.com/watch?v=q1A7ardAe4o


                // id canal:UCcyeS-u6ErEO-Az7J56DsIg

                // https://www.googleapis.com/youtube/v3/search?key=AIzaSyAbYQeSBSSwsawljAP4liZSU9GbLLQW5-c&channelId=UCcyeS-u6ErEO-Az7J56DsIg&part=snippet,id&order=date&maxResults=5
                contador = contador + 1;
            });
        });
}
function cambiaPagina(param) {
    var numero = parseInt(document.getElementById("text").value);
    miOtroAjax(numero);
    if (param == 1) {
        numero = numero + 1;
        if (numero > 10) {
            numero = 1;
        }
    } else {
        numero = numero - 1;
        if (numero < 1) {
            numero = 10;
        }
    }
    document.getElementById("text").value = numero;
}
