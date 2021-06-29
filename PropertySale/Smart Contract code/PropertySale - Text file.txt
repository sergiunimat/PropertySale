pragma solidity ^0.8.1;


contract PropertSale {
    address public ownerAddress;
    uint public percentageFee;
    
    struct PropertyObj {
        string id;
        uint weiPrice;
        address owner;
    }
    
    
    PropertyObj[] public PropertyArray;
    
    constructor(){
        ownerAddress = msg.sender;
        percentageFee = 5;
    }
    
    /***************** Helper functions ******************/
    function checkIfPropertyExists(string memory _propertyId)private view returns(bool){
        for(uint i = 0; i<PropertyArray.length; i++){
            if (keccak256(bytes((PropertyArray[i].id))) == keccak256(bytes((_propertyId)))) {
                return true;
            }
        }
        return false;
    }
    
    
    function checkIfAddressIsOwnedByOwnerAccount(string memory _propertyId)public view returns(bool){
        for(uint i = 0; i<PropertyArray.length; i++){
            if ((keccak256(bytes((PropertyArray[i].id))) == keccak256(bytes((_propertyId)))) &&
            (address(PropertyArray[i].owner)==ownerAddress)) {
                return true;
            }
        }
        return false;
    }
    
    function checkIfPropertyExistsAndisOwnedByTheSeller(string memory _propertyId, address _sellerAddress)public view returns(bool){
        for(uint i = 0; i<PropertyArray.length; i++){
            if ((keccak256(bytes((PropertyArray[i].id))) == keccak256(bytes((_propertyId)))) &&
            (address(PropertyArray[i].owner)==_sellerAddress)) {
                return true;
            }
        }
        return false;
    }
    
    
    function getPropertyIndexByid(string memory _propertyId)private view returns(uint){
        for(uint i = 0; i<PropertyArray.length; i++){
            if (keccak256(bytes((PropertyArray[i].id))) == keccak256(bytes((_propertyId)))) {
                return i;
            }
        }
    }
    
    function getPropertyWeiPriceByid(string memory _propertyId)public view returns(uint){
        for(uint i = 0; i<PropertyArray.length; i++){
            if (keccak256(bytes((PropertyArray[i].id))) == keccak256(bytes((_propertyId)))) {
                return PropertyArray[i].weiPrice;
            }
        }
    }
    
    function transferProperty(address _to, string memory _propertyId) public returns(bool){
        require(checkIfPropertyExists(_propertyId)==true,'The estate property is not registered on the smart contract');
        PropertyArray[getPropertyIndexByid(_propertyId)].owner=_to;
    }

    
    /***************** External functions ******************/
    
    
    function addProperty(string memory _propertyId,uint _weiPrice) public{
        require(msg.sender == ownerAddress, 'Property can be added only thorough the comapany.');
        require(checkIfPropertyExists(_propertyId)==false,'The property at this Id has been already added.');
        PropertyArray.push(PropertyObj(_propertyId,_weiPrice,msg.sender));
    }
    
    
    
    function editPropertyPrice(string memory _propertyId,uint _weiPrice)public{
        require(PropertyArray[getPropertyIndexByid(_propertyId)].owner==msg.sender,'Only the owner of the property can change the property price');
        require(checkIfPropertyExists(_propertyId)==true,'This property does not exist.');
        PropertyArray[getPropertyIndexByid(_propertyId)].weiPrice = _weiPrice;
    }
    
    function deleteProperty(string memory _propertyId)public{
        require(PropertyArray[getPropertyIndexByid(_propertyId)].owner==msg.sender,'Only the owner of the property can delete the property');
        require(checkIfPropertyExists(_propertyId)==true,'This property does not exist.');
        PropertyArray[getPropertyIndexByid(_propertyId)] = PropertyArray[PropertyArray.length-1];
        PropertyArray.pop();
    }
    
    
    
    /***************** Views ******************/
    function getOwnerAddress()public view returns(address){return ownerAddress;}
    function getArrayOfProperties()public view returns(string memory){
        string memory convertedArray ="";
        for(uint i=0;i<PropertyArray.length;i++){
            convertedArray =string(abi.encodePacked(convertedArray,abi.encodePacked(PropertyArray[i].id,","))); 
        }
        return convertedArray;
    }
    
    
}
