$(document).ready(function () {
    //DELETE-FEATURE
    $(document).on("click", ".delete-service", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE-PARTNERSHIP
    $(document).on("click", ".delete-partnership", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
  
    //DELETE-POSITION
    $(document).on("click", ".delete-position", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //DELETE-EMPLOYEE
    $(document).on("click", ".delete-employee", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
   
    //DELETE-NEWS
    $(document).on("click", ".delete-news", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE-BRAND
    $(document).on("click", ".delete-brand", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
   
    //DELETE-LIQUOR-COLOR
    $(document).on("click", ".delete-liquorcolor", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //DELETE-LIQUOR-FLAVOR
    $(document).on("click", ".delete-liquorflavor", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //DELETE-WINE-FOOD-PAIRING
    $(document).on("click", ".delete-winefoodpairing", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //DELETE-TYPE-PRODUCT
    $(document).on("click", ".delete-typeproduct", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //DELETE-TYPE-PRODUCT
    $(document).on("click", ".delete-product", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE-TYPE-PRODUCT
    $(document).on("click", ".delete-review", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE-MESSAGE
    $(document).on("click", ".delete-message", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected File Deleted Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })
    
    //MARK AS READ MESSAGE
    $(document).on("click", ".markasread-message", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Mark as Read!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Marked as Read!',
                            'Selected Message Marked as read Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Selected Messagge could not Marked as read Successfully',
                            'success'
                        )
                        location.reload();
                    }
                })
            }
        })
    })

    //DELETE ALL-SAMETYPE-PRODUCT FROM BASKET
    $(document).on("click", ".basket-remove-all", function (e) {
        e.preventDefault();
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path)
                    .then(function (response) {
                        if (!response.ok) {
                            alert("Error!")
                        }
                        return response.text();
                    }).then(data => {
                        $(".basket-container").html(data);
                        let productcounter = $(".basket-counter-view").val();
                        $(".basket-count").text(productcounter);
                    });
                fetch(path).then(Response => {
                        Swal.fire(
                            'Deleted!',
                            'Selected File Deleted Successfully',
                            'success'
                        ).then(function () {
                            location.reload();
                        }).then(data => {
                $(".basket-container").html(data);
                let productcounter = $(".basket-counter-view").val();
                $(".basket-count").text(productcounter);
                });
                })
            }
        })
      
    })
    //LOG-OUT USER
    $(document).on("click", ".logout-user", function (e) {
        e.preventDefault();
        console.log("salamlog");
        let path = $(this).attr("href")

        Swal.fire({
            title: 'Are You Sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Log Out!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(path).then(Response => {
                    if (Response.ok) {
                        Swal.fire(
                            'Logged Out!',
                            'Logged Out Successfully',
                            'success'
                        ).then(function () {
                            window.location = Response.url;
                        })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Failed to Log Out!',
                            'success'
                        )
                    }
                })
            }
        })
    })
});
