 //PROGRESS-BAR COMMENT REVIEW
 let progressContainerfive = document.querySelector('.star-five-container');
 let progressContainerfour = document.querySelector('.star-four-container');
 let progressContainerthree = document.querySelector('.star-three-container');
 let progressContainertwo = document.querySelector('.star-two-container');
 let progressContainerone = document.querySelector('.star-one-container');

 setPercentage();
 function setPercentage() {
    //Star Five
    let percentagefive = progressContainerfive.getAttribute('data-percentage') + '%';
    let progressElfive = document.querySelector('.star-five');
    progressElfive.style.width = percentagefive;
    //Star Four
    let percentagefour = progressContainerfour.getAttribute('data-percentage') + '%';
    let progressElfour = document.querySelector('.star-four');
    progressElfour.style.width = percentagefour;
    //Star Three
    let percentagethree = progressContainerthree.getAttribute('data-percentage') + '%';
    let progressElthree = document.querySelector('.star-three');
    progressElthree.style.width = percentagethree;
    //Star Two
    let percentagetwo = progressContainertwo.getAttribute('data-percentage') + '%';
    let progressEltwo = document.querySelector('.star-two');
    progressEltwo.style.width = percentagetwo;
    //Star One
    let percentageone = progressContainerone.getAttribute('data-percentage') + '%';
    let progressElone = document.querySelector('.star-one');
    progressElone.style.width = percentageone;
 }

