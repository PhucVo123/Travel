$('#txtKeyword').keyup(function () {
    var searchField = $('#txtKeyword').val();
    var expression = RegExp(searchField, "i");

    $.ajax({
        type: "GET",
        url: "/Service/Search",
        success: function (response) {
            var data = JSON.parse(response);
            console.log(data);
            if (searchField != "")
            {
                var html_body = `
                <div class="tt-dataset tt-dataset-states">

                </div>`;
            }
            $.each(data, function (key, item) {
                if (item.position.search(expression) != -1 && searchField != "") {
                    var html_Search =
                        `<div class="man-section tt-suggestion tt-selectable">
                              <div class="image-section">
                                    <img src="${item.img}">
                              </div>
                              <div class="description-section">
                                    <h1>${item.position}</h1><p>${item.namePackage}</p>
                                    <span>
                                      <i class="fa fa-clock-o" aria-hidden="true">
                                      </i> 12:00 PM <i class="fa fa-map-marker" aria-hidden="true"></i> ${item.position}</span>
                                      <div class="more-section">
                                          <a href="#" target="_blank">
                                            <button>More Info</button>
                                          </a>
                                      </div>
                                </div>
                            <div style="clear:both;"></div>
                        </div>`;
                        ${'.tt-dataset'}.append(html_Search);
                }
            })
        }
    })
})