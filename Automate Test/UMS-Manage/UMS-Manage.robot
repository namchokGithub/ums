# มอดูลจัดการผู้ใช้ง่านเว็บไซต์
*** Settings ***
Library     Selenium2Library

*** Variables ***
${ums_url}                          https://localhost:44346/
${ums_manage_url}                   https://localhost:44346/ManageUser
${ums_login_input_email}            xpath=//*[@id="acc_Email"]
${ums_login_input_password}         xpath=//*[@id="acc_Password"]
${ums_btn_login}                    xpath=//*[@id="account"]/div[3]/div[2]/button
${manage_btn_edit}                  xpath=//*[@id="showAllUserTable"]/tbody/tr[1]/td[7]/button[1]
${manage_btn_delete}                xpath=//*[@id="showAllUserTable"]/tbody/tr[1]/td[7]/button[2]
${manage_input_name}                xpath=//*[@id="acc_Firstname"]
${manage_input_lastname}            xpath=//*[@id="acc_Lastname"]
${manage_input_role}                xpath=//*[@id="acc_RoleId"]
${manage_btn_edit_save}             xpath=//*[@id="EditUser"]/div/div/div[3]/button[2]
${manage_btn_edit_back}             xpath=//*[@id="EditUser"]/div/div/div[3]/button[1]
${manage_delete_btn_confirm}        xpath=/html/body/div[3]/div/div[3]/button[1]
${manage_delete_btn_back}           xpath=/html/body/div[3]/div/div[3]/button[2]

*** Keywords ***
Open web browser
    Open Browser  ${ums_url}     chrome
    Maximize Browser Window

Login with "${username}" "${password}"
    Input Text          ${ums_login_input_email}        ${username}
    Input Password      ${ums_login_input_password}     ${password}
    sleep                       1s
    Click Element       ${ums_btn_login}

Go to Manage User
    Go to       ${ums_manage_url}

Edit 
    Click Element       ${manage_btn_edit}
    sleep                       1s

Delete 
    Click Element       ${manage_btn_delete}
    sleep                       1s

Fill firstname "${firstname}"
    Input Text      ${manage_input_name}    ${firstname}

Fill lastname "${lastname}"
    Input Text      ${manage_input_lastname}    ${lastname}

# 1 = Admin
# 2 = User
Select role "${role_value}"
    Select From List By Value       ${manage_input_role}          ${role_value}

Save edit user
    sleep                       1s
    click element               ${manage_btn_edit_save} 

Confirm delete
    sleep                       1s
    Click Element       ${manage_delete_btn_confirm}

Cancle delete
    sleep                       1s
    Click Element       ${manage_delete_btn_back}

# success = "Update user account successfully!"
The alert message must say "${message}"
    Wait Until Page Contains    ${message}


*** Test Cases ***
# UMS-Manage-01 แก้ไขข้อมูลผู้ใช้งาน
UMS-Manage-01-01
    [Documentation]     กรอกชื่อจริง นามสกุล และเลือกบทบาทผู้ใช้งานถูกต้อง
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname "Athiruj"
    AND Fill lastname "LastAthiruj"
    AND Select role "2"
    AND Save edit user
    THEN The alert message must say "Update user successfully!"
    [Teardown]    Close Browser

UMS-Manage-01-02
    [Documentation]     กรอกชื่อจริง นามสกุล และเลือกบทบาทผู้ใช้งานไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname "!Athiruj"
    AND Fill lastname "!LastAthiruj"
    AND Select role "2"
    AND Save edit user
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Manage-01-03
    [Documentation]     กรอกชื่อจริงไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname "!Athiruj"
    AND Fill lastname "LastAthiruj"
    AND Save edit user
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Manage-01-04
    [Documentation]     กรอกนามสกุลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname "Athiruj"
    AND Fill lastname "!LastAthiruj"
    AND Save edit user
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Manage-01-05
    [Documentation]     กรอกชื่อจริง และเลือกบทบาทผู้ใช้งาน แต่ไม่กรอกนามสกุล
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname "Athiruj"
    AND Fill lastname " "
    AND Save edit user
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Manage-01-06
    [Documentation]      กรอกนามสกุล และเลือกบทบาทผู้ใช้งาน แต่ไม่กรอกชื่อจริง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Edit
    AND Fill firstname " "
    AND Fill lastname "LastAthiruj"
    AND Save edit user
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser

# UMS-Manage-02 ลบข้อมูลผู้ใช้งาน
UMS-Manage-02-01
    [Documentation]      ยกเลิกการลบช้อมูลผู้ใช้งาน
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Delete
    THEN Cancle delete
    [Teardown]    Close Browser

UMS-Manage-02-02
    [Documentation]      ลบข้อมูลผู้ใช้งานสำเร็จ
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Manage User
    AND Delete
    AND Confirm delete
    THEN The alert message must say "Delete user account successfully!"
    [Teardown]    Close Browser