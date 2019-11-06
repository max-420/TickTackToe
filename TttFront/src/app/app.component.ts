import { Component } from '@angular/core';
import {HttpService} from "./services/http.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  title = 'ttt-front';
  matrix: any = [];
  gameId: number;
  userSide: string;
  winner: boolean;
  isFinished: boolean;
  isError: boolean;
  constructor(
    private httpService: HttpService,
  ) {
  }

  startGame() {
    this.isError = false;
    if(this.gameId && !this.isFinished){
      this.endGame();
    }
    this.httpService.post('game', {}).subscribe(data => {
      this.isFinished = false;
      this.gameId = data.gameId;
      for(var i = 0; i < 3; i++)
      {
        this.matrix[i] = [];
        for(var j = 0; j < 3; j++)
        {
          this.matrix[i][j] = "";
        }

      }
      if(data.coordinates)
      {
        this.matrix[data.coordinates.x][data.coordinates.y] = 'X';
        this.userSide = 'O';
      }
      else {
        this.userSide = 'X';
      }
    },
    error => {
        this.isError = true;
    });
  }

  makeStep(x, y) {
    this.isError = false;
    if(this.isFinished || this.matrix[x][y])
    {
      return;
    }
    this.matrix[x][y] = this.userSide;
    this.httpService.put('game', {
      gameId: this.gameId,
      coordinates: {
        x: x,
        y: y,
      }
    }).subscribe(data => {
      if(data.botStep)
      {
        this.matrix[data.botStep.x][data.botStep.y] = this.userSide === 'X' ? 'O' : 'X'
      }
      this.winner = data.winner;
      this.isFinished = data.isFinished;
    },
    error => {
        this.isError = true;
    });
  }

  endGame() {
    this.isError = false;
    if (this.isFinished) {
      return;
    }
    this.httpService.put('game/end', this.gameId)
      .subscribe(data => {
        this.isFinished = true;
      },
      error => {
        this.isError = true;
      });
  }
}
