import { Component, OnInit, ViewChild } from '@angular/core';

import { ScaleService } from 'src/app/services/scale.service';

@Component({
  selector: 'app-scale',
  templateUrl: './scale.component.html',
  styleUrls: ['./scale.component.scss']
})
export class ScaleComponent implements OnInit {
  public file: File;
  public submitted: boolean = false;

  @ViewChild('uploadControl') fileControl;

  constructor(private scaleService: ScaleService) { }

  ngOnInit() {
  }

  onFileChanged() {
    if (this.fileControl.nativeElement.files.length === 0) {
      window.alert('Selecteer een bestand a.u.b..');
    } else if (this.fileControl.nativeElement.files.length === 1) {
      this.file = this.fileControl.nativeElement.files[0];
    }
  }

  onSubmit() {
    this.submitted = true;
    this.scaleService.upload(this.file).subscribe(() => {
      this.fileControl.nativeElement.value = '';
      this.submitted = false;
    }, (err) => {
      console.log(err);
      this.submitted = false;
    });
  }
}