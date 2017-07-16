var xlsuploader = (function () {
    function xlsuploader() {                
        this.elBrowseButton = $("#browseBtnUpload");
        this.elInput = $("#inputUpload");
        this.elButtonUpload = $("#buttonUpload");        
        this.elFileName = $("#fileNameUpload");
        this.elModalForm = $("#modalForm");
        this.elInputComment = $("#inputComment");
        this.elImageUrl = $("#XlsUrl");
        
        this.reader = new FileReader();
        this.file = null;
        this.dataSubmit = null;

        this.url = "/FileHandler/Upload"
        this.urltodb = "/FileHandler/LoadToDb?filename="

        this.init();
    }

    xlsuploader.prototype.init = function () {
        self = this;

        this.elButtonUpload.prop("disabled", true);        

        NProgress.configure({
            parent: "#progressUpload"
            , showSpinner: false
        });

        this.elInput.fileupload({
            url: this.url
            , dataType: 'json'
            , autoUpload: false
            , singleFileUploads: true
            , acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
            , add: function (e, data) { self.addDataSubmit(e, data) }
            , start: function () { self.startUpload() }
            , done: function (e, data) { self.doneUpload(e, data) }
        });
        
        this.elBrowseButton.on('click', function (e) { self.browse(e) });
        this.elButtonUpload.on('click', function () { self.submit() });

        this.elInput.on('change', function (e) { self.inputChange(e) });
        this.reader.onload = function (e) { self.onLoad(e) };
    }

    xlsuploader.prototype.browse = function (e) {
        this.dataSubmit = null;
        this.elInput.html(this.elInput.html());
    }

    xlsuploader.prototype.submit = function () {
        if (this.dataSubmit != null)
            this.dataSubmit.submit();
    }

    xlsuploader.prototype.inputChange = function (e) {
        var el = e.currentTarget;
        var path = el.value;
        this.elFileName.val(path.replace(/^.*[\\\/]/, ''));
        this.reader.readAsDataURL(el.files[0]);
    }

    xlsuploader.prototype.onLoad = function (e) {        
        this.elButtonUpload.prop("disabled", false);        
    }

    xlsuploader.prototype.startUpload = function () {
        NProgress.start();
    }

    xlsuploader.prototype.addDataSubmit = function (e, data) {
        this.dataSubmit = data;
    }

    xlsuploader.prototype.doneUpload = function (e, data) {
        self = this;

        if (data.result.files.length > 0) {
            this.elImageUrl.val(data.result.files[0].url);
            filename = data.result.files[0].name;
            if ($.type(filename) === "string")
                $.get(this.urltodb + filename + '&comment=' + self.elInputComment.val() , function () { self.doneLoadToDB() });
        }
        else
            this.elImageUrl.val("");
    }
    
    xlsuploader.prototype.doneLoadToDB = function () {
        self = this;

        NProgress.done(true);

        setTimeout(function () {
            self.elModalForm.modal("hide");
        }, 250);
    }

    return xlsuploader;
})();