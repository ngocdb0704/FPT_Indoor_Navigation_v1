
//get MapPoints ByMapId
var mappointList = [];
var mappointIDList = [];

var mapidTake;
var flooridTake;
var buildingidTake;
function getMapPointsByMapId(mapId, mappointid) {
    $.ajax({
        url: `https://finnsapi.developvn.click/api/mappoints?filter=mapId eq ${mapId}`,
        method: "get",
    }).then(function (mappointdata) {
        $('#map-list').empty();
        mappointList = [];
        mappointIDList = [];
        mappointdata.forEach(function (mappoint) {
            var locationAppParse = parseLocation(mappoint.locationApp);
            mappointList.push({
                id: mappoint.mapPointId,
                name: mappoint.mappointName,
                x: locationAppParse.x,
                y: locationAppParse.y
            });
            const mappointrow = `
                    <tr>
                        <td><input type="checkbox" class="checkbox"></td>
                         <td>#${mappoint.mapPointId}</td>
                         <td>
                       <div class="d-flex align-items-center">
                         <div class="avatar avatar-image avatar-sm m-r-10">
                               <img src="/Images/Mappoint/${mappoint.image}" alt="">
                                 </div>
                             <h6 class="m-b-0">${mappoint.mappointName}</h6>
                                 </div>
                        </td>
                      <td>${mappoint.locationWeb}</td>
                          <td>${mappoint.locationApp}</td>
                          <td class="text-right">
                           <button class="btn btn-icon btn-danger btn-hover btn-sm btn-rounded mappoint-delete" data-id=${mappoint.mapPointId}>
                             <i class="anticon anticon-delete"></i>
                          </button>
                        </td>
                      </tr>
                       `;
            // thêm hàng mới vào tbody
            $('#map-list').append(mappointrow);
        });
        renderPoints(mappointList, mappointid);
        console.log("mappoint legh:", mappointList);
        console.log("mappointid:", mappointid);

        mapidTake = mapId;
        buildingidTake = buildingid;
        flooridTake = floorid;

        console.log("MapId:", mapId);
        console.log("BuildingId:", buildingid);
        console.log("FloorId:", floorid);

    }).catch(function (error) {
        console.error("error occurred while fetching mappoint data:", error);
    });
}

//Delete mappoint
$(document).on('click', '.mappoint-delete', function () {
    var mapPointId = $(this).data('id');

    // Hiển thị cửa sổ xác nhận của SweetAlert
    Swal.fire({
        title: 'Are you sure?',
        text: 'You are about to delete this map point. This action cannot be undone.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            deleteMapPoint(mapPointId);
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire('Cancelled', 'Your map point is safe :)', 'info');
        }
    });
});

//function delete
function deleteMapPoint(mapPointId) {
    $.ajax({
        url: `https://finnsapi.developvn.click/api/mappoints/${mapPointId}`,
        type: 'DELETE',
        success: function (response) {
            console.log('Map point deleted successfully:', response);
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Map point deleted successfully!'
            }).then((result) => {
                // Tải lại trang sau khi đóng hộp thoại SweetAlert
                location.reload();
            });
        },
        error: function (xhr, status, error) {
            console.error('Error deleting map point:', error);
            // Hiển thị SweetAlert thông báo lỗi
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to delete map point. Please try again later.'
            });
        }
    });

}


//add mappoint
function addMapPoint() {
    var mappointName = $('#mapPointName').val();
    var xCoordinate = $('#mapPointX').val();
    var yCoordinate = $('#mapPointY').val();

    //Lưu ý t để ngược bởi vì khi cho vào data base thì x với y bị đảo ngược
    var coordinatesString = '[' + yCoordinate + ',' + xCoordinate + ']';

    var formData = new FormData();
    // Thêm các trường dữ liệu vào formData
    formData.append('MappointName', mappointName);
    formData.append('LocationWeb', coordinatesString);
    formData.append('LocationApp', coordinatesString);
    formData.append('LocationGps', '[0,0]');
    formData.append('FloorId', flooridTake);
    formData.append('BuildingId', buildingidTake);
    formData.append('MapId', mapidTake);

    $.ajax({
        url: 'https://finnsapi.developvn.click/api/mappoints',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            console.log('Map point added successfully:', response);
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Map point added successfully!'
            }).then(function () {
                location.reload();
            });
            $('#add-MapPoint-modal').modal('hide');
        },
        error: function (xhr, status, error) {
            console.error('Error adding map point:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to add map point. Please try again later.'
            });
            // Xử lý lỗi khi không thể thêm map point
        }
    });
}

//show Coordinate in addform
function showSelectedPointInAddForm() {
    if (selectedPoint) {
        // Lấy giá trị x và y từ selectedPoint
        var xCoordinate = selectedPoint.x.toFixed(2);;
        var yCoordinate = selectedPoint.y.toFixed(2);

        $('#mapPointX').val(xCoordinate);
        $('#mapPointY').val(yCoordinate);
    } else {
        // Nếu selectedPoint không có giá trị, thông báo lỗi
        console.error("Selected point is undefined or null.");
    }
    $('#add-MapPoint-modal').modal('show');
}

//Convert string thành tọa dộ
function parseLocation(locationWebString) {
    // Loại bỏ dấu ngoặc vuông và dấu cách ở hai đầu chuỗi
    var cleanedString = locationWebString.replace(/^\[|\]$/g, '');

    // Tách chuỗi thành mảng các phần tử, sử dụng dấu phẩy làm dấu phân cách
    var coordinates = cleanedString.split(',');

    // Chuyển đổi phần tử mảng thành số thực
    var x = parseFloat(coordinates[0]);
    var y = parseFloat(coordinates[1]);

    return { x: x, y: y };
}



const sampleEdge = {
    edgeId: "", pointId1: "", pointId2: "", direction: 2, edgeLength: 0
}


//=============================================================================================================================

const canvas = document.getElementById("canvas_data");
const context = canvas.getContext("2d");

var allEdges = [];



//ratio of image's width, length vs image's pixels
var ratio = 8.682926829;
var root = { id: "root", x: 628, y: 160 };
var radius = 5;

//var ratio = 17.696;
//var root = { id: "root", x: 1290, y: 6 };
//var radius = 5 * Math.sqrt(ratio) / 2;

//Start and end point of 1 edge
var beginPoint = { id: "", x: 0, y: 0 };
var endPoint = { id: "", x: 0, y: 0 };
//nearbyPoint checks out if there is a nearby point of begin and end point
//if distance > nearbyPoint radius then input of beginPoint does not accept
var nearbyPoint = { id: "", x: 0, y: 0 };
var numberOfClicks = 0;

//luu y
//toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do Y
//cac function sau phuc vu cho viec lay toa do 
//toa do trong anh tinh theo goc toa do cua database
function pixelLocation(event) {
    document.getElementById("demo").innerHTML = "LocationWeb1: (" + (event.offsetX) + ", " + (event.offsetY) + ")" +
        "LocationWebRoot: (" + (event.offsetX - root.x) + ", " + -(event.offsetY - root.y) + ")";
}
//toa do cua diem trong database
function databaseLocation(event) {
    document.getElementById("demo").innerHTML = document.getElementById("demo").innerHTML +
        " <br>  LocationApp: (" + (event.offsetX - root.x) / ratio + ", " + -(event.offsetY - root.y) / ratio + ")";
}

function setEditmappoint() {
    console.log("SETTING EDIT MAPPOINT");
    canvas.setAttribute("onclick", "ChooseEditMappoint(event), count(event)");
}
function ChooseEditMappoint(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        if (inButtonRange(mappointList, beginPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(mappointList, beginPoint);
            beginPoint = nearbyPoint;
            var mappointInfo = "Mappoint hiện tại: " + beginPoint.name + "\n";
            mappointInfo += "Tọa độ: (" + (event.offsetX - root.x) / ratio + ", " + (event.offsetY - root.y) / ratio + ")\n";
            document.getElementById("editInfo").innerText = mappointInfo;
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };

        }
    }
    //3r click will be endPoint
    if (numberOfClicks == 1) {
        endPoint.x = event.offsetX;
        endPoint.y = event.offsetY;
        //ve hinh tron mau do
        context.beginPath();
        context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
        context.closePath();
        context.fillStyle = 'orange';
        context.fill();
        // Check if endPoint is within map point range
        if (inButtonRange(mappointList, endPoint)) {
            // If endPoint is within map point range, reset variables and return
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = nearbyPoint;
            endPoint = nearbyPoint;
            return;
        }
        else {
            // Draw red circle at endPoint
            context.beginPath();
            context.arc(endPoint.x, endPoint.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'red';
            context.fill();

            // Draw line between beginPoint and endPoint
            context.lineWidth = 1;
            context.beginPath();
            let bX = beginPoint.x * ratio + root.x;
            let bY = -beginPoint.y * ratio + root.y;

            context.moveTo(bX, bY);
            context.lineTo(endPoint.x, endPoint.y);
            context.stroke();

            showInEditForm(beginPoint, endPoint);

            // Reset variables for next execution
            numberOfClicks = -1;
            beginPoint = { id: "", x: 0, y: 0 };
            endPoint = { id: "", x: 0, y: 0 };
            nearbyPoint = { id: "", x: 0, y: 0 };
            canvas.setAttribute("onclick", "");
        }
    }
}

function showInEditForm(beginPoint, endPoint) {

    if (endPoint) {
        // Lấy giá trị x và y từ selectedPoint
        let xCoordinate = ((endPoint.x - root.x) / ratio).toFixed(2);
        let yCoordinate = (-(endPoint.y - root.y) / ratio).toFixed(2);

        //do khi cho vào database bị ngược
        var coordinatesString = '[' + yCoordinate + ',' + xCoordinate + ']';

        var mappointName = findNameInMapPointList(beginPoint.id, mappointList);
        var mappointId = findIdInMapPointList(beginPoint.id, mappointList);
        var mappointData = findCoordinatesById(mappointId, mappointList)

        $('#update-mapPointId').val(mappointId);
        $('#update-mapPointName').val(mappointName);
        $('#update-mapPointOldLocation').val('[' + mappointData.x + ',' + mappointData.y + ']');
        $('#update-mapPointXCoordinate').val(xCoordinate);
        $('#update-mapPointYCoordinate').val(yCoordinate);
    } else {
        // Nếu selectedPoint không có giá trị, thông báo lỗi
        console.error("Selected point is undefined or null.");
    }
    $('#update-MapPoint-modal').modal('show');
}

function editMapPoint() {
    var mappointId = $('#update-mapPointId').val();
    var mappointName = $('#update-mapPointName').val();
    var xCoordinate = $('#update-mapPointXCoordinate').val();
    var yCoordinate = $('#update-mapPointYCoordinate').val();
    var newLocation = $('#update-mapPointNewLocation').val();
    var imageInput = $('#update-mapPointImage');

    var coordinatesString = '[' + yCoordinate + ',' + xCoordinate + ']';
    var formData = new FormData();
    // Thêm các trường dữ liệu vào formData
    formData.append('MapPointId', mappointId);
    formData.append('MappointName', mappointName);
    formData.append('LocationWeb', coordinatesString);
    formData.append('LocationApp', coordinatesString);
    formData.append('LocationGps', '[0,0]');
    formData.append('FloorId', flooridTake);
    formData.append('BuildingId', buildingidTake);
    formData.append('MapId', mapidTake);
    if (imageInput[0].files.length > 0) {
        var imageFile = imageInput[0].files[0];
        formData.append('Image', imageFile);
    }

    $.ajax({
        url: 'https://finnsapi.developvn.click/api/mappoints/' + mappointId,
        type: 'PUT',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            console.log('Map point update successfully:', response);
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Map point update successfully!'
            }).then(function () {
                location.reload();
            });
        },
        error: function (xhr, status, error) {
            console.error('Error adding map point:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to add map point. Please try again later.'
            });
        }
    });
}
function setDeleteMappoint() {
    console.log("SETTING Delete MAPPOINT");
    canvas.setAttribute("onclick", "ChooseDeleteMappoint(event), count(event)");
}
function ChooseDeleteMappoint(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        if (inButtonRange(mappointList, beginPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(mappointList, beginPoint);
            beginPoint = nearbyPoint;
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };


        }
        var mappointID = beginPoint.id;
        Swal.fire({
            title: 'Are you sure?',
            text: 'You are about to delete this map point. This action cannot be undone.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                deleteMapPoint(mappointID);
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire('Cancelled', 'Your map point is safe :)', 'info');
            }
        });

        context.beginPath();
        context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
        context.closePath();
        context.fillStyle = 'orange';
        context.fill();

        //reset
        numberOfClicks = -1;
        beginPoint = { id: "", x: 0, y: 0 };
        nearbyPoint = { id: "", x: 0, y: 0 };
        canvas.setAttribute("onclick", "");
    }

}

//tìm dc id trong mappoint list đó
function findIdInMapPointList(id, mappointList) {
    for (var i = 0; i < mappointList.length; i++) {
        if (mappointList[i].id === id) {
            return mappointList[i].id; // Trả về id nếu được tìm thấy trong mảng
        }
    }
    return null; // Trả về null nếu id không tồn tại trong mảng
}

//tìm dc name trong mappoint list đó
function findNameInMapPointList(id, mappointList) {
    for (var i = 0; i < mappointList.length; i++) {
        if (mappointList[i].id === id) {
            return mappointList[i].name; // Trả về name nếu được tìm thấy trong mảng
        }
    }
    return null; // Trả về null nếu name không tồn tại trong mảng
}

function findCoordinatesById(id, mappointList) {
    for (var i = 0; i < mappointList.length; i++) {
        if (mappointList[i].id === id) {
            return { x: mappointList[i].x, y: mappointList[i].y }; // Trả về tọa độ nếu id được tìm thấy trong mảng
        }
    }
    return null; // Trả về null nếu không tìm thấy id trong mảng
}

function setEditmappoint() {
    console.log("SETTING EDIT MAPPOINT");
    canvas.setAttribute("onclick", "ChooseEditMappoint(event), count(event)");
}
function ChooseEditMappoint(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        if (inButtonRange(mappointList, beginPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(mappointList, beginPoint);
            beginPoint = nearbyPoint;
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };
        }
    }
    //3r click will be endPoint
    if (numberOfClicks == 1) {
        endPoint.x = event.offsetX;
        endPoint.y = event.offsetY;
        //ve hinh tron mau do
        context.beginPath();
        context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
        context.closePath();
        context.fillStyle = 'orange';
        context.fill();
        // Check if endPoint is within map point range
        if (inButtonRange(mappointList, endPoint)) {
            // If endPoint is within map point range, reset variables and return
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = nearbyPoint;
            endPoint = nearbyPoint;
            return;
        }
        else {
            // Draw red circle at endPoint
            context.beginPath();
            context.arc(endPoint.x, endPoint.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'red';
            context.fill();

            // Draw line between beginPoint and endPoint
            context.lineWidth = 1;
            context.beginPath();
            let bX = beginPoint.x * ratio + root.x;
            let bY = -beginPoint.y * ratio + root.y;

            context.moveTo(bX, bY);
            context.lineTo(endPoint.x, endPoint.y);
            context.stroke();

            // Reset variables for next execution
            numberOfClicks = -1;
            beginPoint = { id: "", x: 0, y: 0 };
            endPoint = { id: "", x: 0, y: 0 };
            nearbyPoint = { id: "", x: 0, y: 0 };
            canvas.setAttribute("onclick", "");
        }
    }
}
function setDeleteMappoint() {
    console.log("SETTING Delete MAPPOINT");
    canvas.setAttribute("onclick", "ChooseDeleteMappoint(event), count(event)");
}
function ChooseDeleteMappoint(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        if (inButtonRange(mappointList, beginPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(mappointList, beginPoint);
            beginPoint = nearbyPoint;
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };

            Swal.fire({
                title: 'Are you sure?',
                text: 'You are about to delete this map point. This action cannot be undone.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {
                    deleteMapPoint(beginPoint.id);
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    Swal.fire('Cancelled', 'Your map point is safe :)', 'info');
                }
            });
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'orange';
            context.fill();

            //reset
            numberOfClicks = -1;
            beginPoint = { id: "", x: 0, y: 0 };
            nearbyPoint = { id: "", x: 0, y: 0 };
            canvas.setAttribute("onclick", "");
        }

    }

}
function filterMapPointsByName(name) {
    $.ajax({
        url: `https://finnsapi.developvn.click/api/mappoints?filter=mappointName eq ${name}`,
        type: 'GET',
        success: function (data) {
            data.forEach(function (mapPoint) {
                console.log('Filtered Map Point ID:', mapPoint.mapPointId);
            });
        },
        error: function (xhr, status, error) {
            console.error('Error filtering Map Points:', error);
        }
    });
}


//tìm dc id trong mappoint list đó
function findIdInMapPointList(id, mappointList) {
    for (var i = 0; i < mappointList.length; i++) {
        if (mappointList[i].id === id) {
            return mappointList[i].id; // Trả về id nếu được tìm thấy trong mảng
        }
    }
    return null; // Trả về null nếu id không tồn tại trong mảng
}


//function duoc goi khi bam nut Connect Edge
function setEdge() {
    console.log("SETTING EDGE");
    canvas.setAttribute("onclick", "drawLine(event), count(event)");
}
window.setEdge = setEdge;

//function duoc goi khi bam Get Mappoints
function renderPoints(points, highlightId) {
    // Lấy dữ liệu từ array points
    points.forEach(point => {
        context.beginPath();
        // Convert coordinates from image pixels to database coordinates
        let pixelX = point.x * ratio + root.x;
        let pixelY = -point.y * ratio + root.y;

        // Draw circle at pixelX,pixelY with radius = 2, start from angle 0, end at 360, counter-clockwise
        if (point.id === parseInt(highlightId, 10)) {
            // Nếu là mappoint cần highlight
            context.arc(pixelX, pixelY, radius, 0, 2 * Math.PI, false);
            context.fillStyle = 'red';
            context.fill();
        } else {
            // Các mappoint khác
            context.arc(pixelX, pixelY, radius, 0, 2 * Math.PI, false);
            context.fillStyle = 'orange';
            context.fill();
        }
    });

    saveCanvasState();

    context.beginPath();
    context.arc(root.x, root.y, radius, 0, 2 * Math.PI, false);
    context.fillStyle = 'blue';
    context.fill();
    context.closePath();
    saveCanvasState();
}
window.renderPoints = renderPoints;

//phuc vu cho drawLine()
function count() {
    numberOfClicks = numberOfClicks + 1;
}


function drawLine(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        if (inButtonRange(mappointList, beginPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(mappointList, beginPoint);
            beginPoint = nearbyPoint;
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };
        }
    }
    //3r click will be endPoint
    if (numberOfClicks == 1) {
        endPoint.x = event.offsetX;
        endPoint.y = event.offsetY;
        //ve hinh tron mau do
        context.beginPath();
        context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
        context.closePath();
        context.fillStyle = 'orange';
        context.fill();
        //truong hop chon khong dung trong pham vi map point
        if (inButtonRange(mappointList, endPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = nearbyPoint;
            endPoint = nearbyPoint;
            return;
        }
        else {
            //lay toa do cua map point (button)
            inButtonRange(mappointList, endPoint);
            endPoint = nearbyPoint;
            nearbyPoint = { id: "", x: 0, y: 0 };
            //bat dau ve 1 duong thang giua beginPoint va endPoint
            context.lineWidth = 1;
            context.beginPath();
            //toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do Y
            let bX = beginPoint.x * ratio + root.x;
            let bY = -beginPoint.y * ratio + root.y;
            let eX = endPoint.x * ratio + root.x;
            let eY = -endPoint.y * ratio + root.y;

            context.moveTo(bX, bY);
            context.lineTo(eX, eY);
            context.stroke();
            //luu gia tri
            saveEdge(beginPoint, endPoint);

            //tra cac bien ve gia tri ban dau de chuan bi cho luot thuc thi tiep theo
            numberOfClicks = -1;
            beginPoint = { id: "", x: 0, y: 0 };
            endPoint = { id: "", x: 0, y: 0 };
            nearbyPoint = { id: "", x: 0, y: 0 };
            canvas.setAttribute("onclick", "");
        }
    }
}

function saveEdge(point1, point2) {
    var edge = {
        edgeId: "1", pointId1: point1.name, pointId2: point2.name, direction: 2, edgeLength: getDistance(point1, point2)
    }
    allEdges.push(edge);
    edge = sampleEdge;
    //luu lai canh vua moi ve
    // const imgData = context.getImageData(0,0,canvas.width,canvas.height);
    saveCanvasState();
    showEdges(allEdges);
    //xoa cac event listener de khong nhan event nao cho den khi bam cac nut functionalities
    canvas.setAttribute("onclick", "");
}


function showEdges(list) {
    document.getElementById("demo").innerHTML = "";
    list.forEach(e => {
        document.getElementById("demo").innerHTML +=
            "<br> Id: " + e.id + ", Start: " + e.pointId1 + ", End: " + e.pointId2 + ", Length: " + e.edgeLength;
    });
}
//Neu trong pham vi cua button thi se tra ve true
//nearbyPoint se lay gia tri cua diem gan nhat neu co
//gia tri se giong trong database

function inButtonRange(allPoints, point) {
    returnPoint = { id: "", x: 0, y: 0 };
    var minDistance = 0.8;
    allPoints.forEach(a => {
        //toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do 
        let x1 = a.x;
        let x2 = (point.x - root.x) / ratio;
        let y1 = a.y;
        let y2 = -(point.y - root.y) / ratio;

        let distance = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        distance = Math.abs(distance);
        if (distance < minDistance) {
            minDistance = distance;
            returnPoint = a;
        }
    });
    if (returnPoint.id == "") {
        return false;
    }
    else {
        //gan gia tri cho nearbyPoint
        //gia tri nay se duoc dung trong function drawLine()
        nearbyPoint = returnPoint;
        return true;
    }
}

//CAC function lien quan den viec undo
const strokeHistory = [];
function saveCanvasState() {
    strokeHistory.push(canvas.toDataURL()); // Save as data URL
}

// Function to undo the last stroke (connect wrong edge)
function undo(value) {
    if (strokeHistory.length > 0) {
        strokeHistory.pop(); // Remove the last stroke
        redrawCanvas();
    }
    //khi undo thi array cac edges cung se phai xoa di
    if (allEdges.length > 0 && value == 1) {
        allEdges.pop();
        showEdges(allEdges);
    }
    canvas.setAttribute("onclick", "");
}

// Function to redraw the canvas from strokeHistory
function redrawCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height); // Clear canvas
    const lastState = new Image();
    lastState.src = strokeHistory[strokeHistory.length - 1];
    lastState.onload = function () {
        context.drawImage(lastState, 0, 0); // Draw the saved state
    };
}

//========================================================


//this function only support mappoints with coordinates in database, not image
function getDistance(point1, point2) {
    return Math.sqrt((point1.x - point2.x) * (point1.x - point2.x) + (point1.y - point2.y) * (point1.y - point2.y));
}

function drawCircle(event) {
    let xX = event.offsetX;
    let yY = event.offsetY;
    context.beginPath();
    context.arc(xX, yY, radius, 0, 2 * Math.PI, false);
    context.lineWidth = 1;
    context.strokeStyle = 'red';
    context.stroke();
}

//=========================================================//=========================================================//
//=========================================================//=========================================================//

var farthestPointImage = { id: "f", x: 0, y: 0 };
var nearbyPointImage = { id: "n", x: 0, y: 0 };

function getFarthestPoint(event) {
    farthestPointImage.x = event.offsetX;
    farthestPointImage.y = event.offsetY;
    document.getElementById("canvas_data").setAttribute("onclick", "");
}
function getNearbyPoint(event) {
    nearbyPointImage.x = event.offsetX;
    nearbyPointImage.y = event.offsetY;
    document.getElementById("canvas_data").setAttribute("onclick", "");
}
function setRoot() {
    undo(false);
    canvas.setAttribute("onclick", "chooseRoot(event)");
}
function chooseRoot(event) {

    //function nay chua xong , neu xong thi se lien quan den get Ratio()
    root.x = event.offsetX;
    root.y = event.offsetY;
    context.beginPath();
    context.arc(root.x, root.y, radius, 0, 2 * Math.PI, false);
    context.fillStyle = 'blue';
    context.fill();
    context.closePath();
    saveCanvasState();
    document.getElementById("demo1").innerHTML = "Root: " + root.x + ", " + root.y;
    getRatio();
    document.getElementById("demo1").innerHTML = document.getElementById("demo1").innerHTML + " <br> Ratio: " + ratio;
    canvas.setAttribute("onclick", "");
}
function getRatio() {
    var irlLength = 125;
    //irl length la tong chieu dai cua anh (tang 1 Al tong chieu dai la 125m)
    //luu y: anh phai duoc cat sat mep nhat co the
    //khong duoc de thua nhieu
    //offsetWidth la chieu dai cua canvas
    //==> ti le bao nhieu pixel/m 
    let x = canvas.offsetWidth;
    ratio = x / irlLength;
}

function resize() {
    var canvas1 = new fabric.Canvas('canvas_data1');
    // Load the background image (you can replace 'your-image.jpg' with your actual image URL)
    fabric.Image.fromURL('/Images/Map/Alpha_tang1.jpg', function (img) {
        // Access the image dimensions
        var scale = canvas1.width / img.width;

        img.set({
            scaleX: scale,
            scaleY: scale,
            originX: 'left',
            originY: 'top'
        });
        // Set canvas dimensions to match the image size
        canvas1.setWidth(canvas1.width * (canvas1.width / img.width));
        canvas1.setHeight(img.height * (canvas1.width / img.width));
        canvas1.setBackgroundImage(img, canvas1.renderAll.bind(canvas1));
        // Add the image to the canvas
        canvas.width = canvas1.width;
        canvas.height = canvas1.height;
        canvas1.setWidth(0);
        canvas1.setHeight(0);
        canvas.style.backgroundImage = 'url("../ALF1new.jpg")';
    });
}

function newMappoint() {
    canvas.setAttribute("onclick", "chooseMappoint(event)");
}
//bien nay se la de luu mappoint moi duoc ghi
var newPoint = { id: "", x: 0, y: 0 };

function chooseMappoint(event) {
    saveCanvasState();

    let x = (event.offsetX - root.x) / ratio;
    let y = -(event.offsetY - root.y) / ratio;

    let canvasCoordinates = `${event.offsetX}, ${event.offsetY}`;
    let databaseCoordinates = `${x.toFixed(2)}, ${y.toFixed(2)}`;

    document.getElementById("locationWeb").value = `[${event.offsetX}, ${event.offsetY}]`;
    document.getElementById("locationApp").value = `[${x.toFixed(2)}, ${y.toFixed(2)}]`;

    context.beginPath();
    context.arc(event.offsetX, event.offsetY, radius, 0, 2 * Math.PI, false);
    context.fillStyle = 'red';
    context.fill();
    context.closePath();

    newPoint = { id: "", x: x, y: y };
    selectedPoint = { x: x, y: y };

    console.log("Map point selected - X:", x, ", Y:", y);
    canvas.setAttribute("onclick", "undo(false),chooseMappoint(event)");
}


//chức năng search mappoint theo name
function search() {
    var ok = false;
    var inputId = document.getElementById("searchMappoint").value.trim().toLowerCase(); // Sử dụng trim và chuyển đổi thành chữ thường
    if (inputId == "") {
        console.log("1111");
        return;
    } else {
        mappointList.forEach(a => {
            if (a.name.toLowerCase().includes(inputId)) { // So sánh cả 2 ở dạng chữ thường
                context.beginPath();
                // Convert coordinates from image pixels to database coordinates
                let pixelX = a.x * ratio + root.x;
                let pixelY = -a.y * ratio + root.y;

                context.arc(pixelX, pixelY, radius, 0, 2 * Math.PI, false);
                context.fillStyle = 'red';
                context.fill();
                context.closePath();
                saveCanvasState();
                ok = true;
                document.getElementById("searchMappoint").value = "";
                return;
            }
        });
    }
    if (!ok) {
        alert("Couldn't find Map point id");
    } else {
        canvas.setAttribute("onclick", "undo(2)");
    }
}


