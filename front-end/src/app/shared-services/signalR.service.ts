import {Injectable} from "@angular/core";
import * as signalR from "@microsoft/signalr";
import {MojConfig} from "../moj-config";
import {DialogServiceService} from "./dialog-service.service";

@Injectable({providedIn: 'root'})
export class SignalRService {

  public static ConnectionID:string | null;
  constructor(private dialogService:DialogServiceService) {
  }
  otvori_ws_konekciju() {
    let connection = new signalR.HubConnectionBuilder()
      .withUrl(`${MojConfig.server_adresa}/hub-putanja?loginTokenId=`)
      .build();

    connection.on("prijem_poruke_js", (p)=>{
      //alert("prijem_poruke_js"+p);
      this.dialogService.openOkDialog(/*"prijem_poruke_js" +*/ p);

    });

    connection
      .start()
      .then(() => {
        SignalRService.ConnectionID=connection.connectionId;
        console.log("konekcija otvorena" + connection.connectionId);
      });
  }

}
