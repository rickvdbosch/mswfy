import { Component, OnInit, ViewChild } from '@angular/core';

import { FilesService } from 'src/app/services/upload.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  public file: File;
  public submitted: boolean = false;

  @ViewChild('uploadControl', null) fileControl;

  constructor(private filesService: FilesService) { }

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
    this.filesService.upload(this.file).subscribe(() => {
      this.fileControl.nativeElement.value = '';
      this.submitted = false;
    }, (err) => {
      console.log(err);
      this.submitted = false;
    });
  }
}