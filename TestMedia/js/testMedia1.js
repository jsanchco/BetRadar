class TestMedia1 {
  constructor(data) {
    this.urlMedia = null;
    this.medias = [];
    this.jwplInstance = null;
    this.avvplInstance = null;
    this.actualPlay = null;
    this.data = data;

    console.log("data ->", data);
  }

  init() {
    $("#submit-form-load").click(() => {
      this.getMedias(event);
    });
  }

  getMedias(event) {
    event.preventDefault();

    const _this = this;
    const url = $("#urlService").val();
    $.ajax({
      url: url,
      type: "GET",
      headers: {
        Accept: "text/plain",
        "Content-Type": "application/json",
      },
      success: function (result) {
        $("#header-list-medias").text(`List Medias ${_this.data.HoraSolicitud}`);
       
	   for (var i = 0; i < result.EventList.length; i++) {

          const eventId = result.EventList[i].id;
          const listStreams = result.EventList[i].streams;
          for (var j = 0; j < listStreams.length; j++) {
            const streamId = listStreams[j].id;
            const listUrls = listStreams[j].UrlList;
            for (var k = 0; k < listUrls.length; k++) {
              const extension = listUrls[k].Key;
              const urlMedia = listUrls[k].Value;
              const listId = _this.medias.map((x) => x.id);
              let id = 1;
              if (listId.length !== 0) {
                id = Math.max(...listId) + 1;
              }

              _this.addMedia({id, eventId, streamId, urlMedia, extension});
            }
          }
        }
      },
      error: function (XMLHttpRequest, textStatus, errorThrown) {
        // var date = Date.parse(_this.data.HoraSolicitud);
        // $("#header-list-medias").text(`List Medias ${_this.data.HoraSolicitud}`);

        // for (var i = 0; i < _this.data.EventList.length; i++) {
          // const eventId = _this.data.EventList[i].id;
          // const listStreams = _this.data.EventList[i].streams;
          // for (var j = 0; j < listStreams.length; j++) {
            // const streamId = listStreams[j].id;
            // const listUrls = listStreams[j].UrlList;
            // for (var k = 0; k < listUrls.length; k++) {
              // const extension = listUrls[k].Key;
              // const urlMedia = listUrls[k].Value;
              // const listId = _this.medias.map((x) => x.id);
              // let id = 1;
              // if (listId.length !== 0) {
                // id = Math.max(...listId) + 1;
              // }

              // _this.addMedia({id, eventId, streamId, urlMedia, extension});
            // }
          // }
        // }

        alert("Error in access to endpoint");
      },
    });
  }

  addMedia(media) {
    this.medias.unshift(media);

    this.addRowToTable(media);

    $(`#button-play${media.id}`).click(() => {
      this.doAction("play", media.id, media.urlMedia, media.extension);
    });
    $(`#button-remove${media.id}`).click(() => {
      this.doAction("remove", media.id, media.urlMedia, media.extension);
    });
  }

  doAction(action, id, url, extension) {
    if (action === "remove") {
      $(`#media-${id}`).remove();
      const findId = this.medias.find((x) => x.id === id);
      if (findId) {
        this.medias.splice(findId, 1);
      }
    }

    if (action === "play") {
      this.configPlayerJWPL(id, url, extension);
      this.configPlayerAVVPL(id, url, extension);
    }
  }

  addRowToTable(media) {
    let urlCut = media.urlMedia;
    if (urlCut.length > 50) {
      urlCut = `${urlCut.substr(0, 50)}...`;
    }

    $("#table-medias tbody").append(
      `<tr id="media-${media.id}">
        <td>${media.id}</td>
        <td>${media.eventId}</td>
        <td>${media.streamId}</td>
        <td>${media.extension}</td>
        <td>${urlCut}</td>
        <td id="cell-date-jwp-${media.id}">
          &nbsp;
        </td>
        <td id="cell-status-jwp-${media.id}">
          &nbsp;
        </td>
        <td id="cell-date-avvp-${media.id}">
          &nbsp;
        </td>
        <td id="cell-status-avvp-${media.id}">
          &nbsp;
        </td>               
        <td>
          <button id="button-play${media.id}" class="btn btn-primary" style="margin-left: 10px">Play</button>
        </td>
      </tr>`
    );
  }

  getActualDate() {
    var dt = new Date();

    return `${dt.getDate().toString().padStart(2, "0")}/${(dt.getMonth() + 1)
      .toString()
      .padStart(2, "0")}/${dt
      .getFullYear()
      .toString()
      .padStart(4, "0")} ${dt
      .getHours()
      .toString()
      .padStart(2, "0")}:${dt.getMinutes().toString().padStart(2, "0")}`;
  }

  configPlayerAVVPL(id, url, extension) {
	  debugger;
    const _this = this;
    const element = $("#playercontainer");

    element.empty();
   		
		element.append(`
        <video>
          <source
              id="source-${id}"
              src="${url}"
          </source>
        </video> 
        `); 

    var config = {
      id: "playercontainer",
    };
    const cellDate = document.getElementById(`cell-date-avvp-${id}`);
    const cellSatus = document.getElementById(`cell-status-avvp-${id}`);

    this.avvplInstance = new avvpl.setupPlayer(config);
    this.avvplInstance.on("error", (error, id) => {
      cellSatus.innerHTML = `error: ${error.message.errorCode}`;
    });
    this.avvplInstance.on("playing", (id) => {
      const date = _this.getActualDate();
      cellDate.innerHTML = date;
      cellSatus.innerHTML = "OK";
    });
  }

  configPlayerJWPL(id, url, extension) {
    const _this = this;

    this.jwplInstance = jwplayer("player").setup({
      file: url,
      // autostart: "viewable",
    });

    const cellDate = document.getElementById(`cell-date-jwp-${id}`);
    const cellSatus = document.getElementById(`cell-status-jwp-${id}`);

    this.jwplInstance.on("error", () => {
      // cellSatus.innerHTML = `error: ${error.sourceError.code}`;
      cellSatus.innerHTML = `error`;
    });
    this.jwplInstance.on("play", () => {
      const date = _this.getActualDate();
      cellDate.innerHTML = date;
      cellSatus.innerHTML = "OK";
    });

    this.jwplInstance.play();
  }

  formatDate(args) {
    if (args === null || args === "") {
      return "";
    }

    let day = args.getDate();
    if (day < 10) day = "0" + day;

    const month = args.getMonth() + 1;
    const year = args.getFullYear();

    if (month < 10) {
      return `${day}/0${month}/${year}`;
    } else {
      return `${day}/${month}/${year}`;
    }
  }
}
