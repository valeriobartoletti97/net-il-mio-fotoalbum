Un fotografo vuole mostrare agli utenti le foto pi� belle che ha scattato e ci chiede di realizzare una webapp che permetta questo.

Ha bisogno di un�area di amministrazione per gestire le foto, quindi
 - vedere tutte quelle inserite (filtrabili)
 - vedere i dettagli di una singola foto
 - aggiungerne di nuove (con validazione)
 - modificarle (con validazione)
 - cancellarle

 Ovviamente queste operazioni pu� svolgerle solo lui, quindi l�accesso alle pagine deve essere protetto da autenticazione.
 Una foto contiene almeno le seguenti informazioni :
 - titolo
 - descrizione
 - immagine (upload)
 - visibile
 - categorie

Una foto pu� essere collegata a pi� categorie, e una categoria pu� essere collegata a pi� foto.

Prevedere quindi anche una semplice pagina di lista, creazione e cancellazione categorie.

Deve essere presente anche una homepage pubblica, nella quale le foto (visibili) sono mostrate agli utenti.
Devono essere filtrabili per titolo.

Prevedere sempre nell�homepage pubblica un semplice form di contatto avente i campi email e messaggio.

Il click sul tasto invia far� partire una richiesta a una nuova api che salver� sul database il messaggio inviato.

L�area di amministrazione va realizzata sfruttando Razor + controller MVC.
La pagina pubblica e l�invio dei messaggi di contatto devono essere gestiti tramite javascript + webapi.

