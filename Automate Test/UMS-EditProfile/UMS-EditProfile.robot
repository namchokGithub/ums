# มอดูลแก้ไขข้อมูลส่วนตัว
*** Settings ***
Library     Selenium2Library

*** Variables ***
${ums_url}                          https://localhost:44346/
${ums_editprofile_url}              https://localhost:44346/EditProfile/Index/adminidtempfortestsystem2020
${ums_login_input_email}            xpath=//*[@id="acc_Email"]
${ums_login_input_password}         xpath=//*[@id="acc_Password"]
${ums_btn_login}                    xpath=//*[@id="account"]/div[3]/div[2]/button
${editprofile_input_name}           xpath=//*[@id="acc_Fname"]            
${editprofile_input_lastname}       xpath=//*[@id="acc_Lname"]
${editprofile_input_curpass}        xpath=//*[@id="acc_Current"]
${editprofile_input_newpass}        xpath=//*[@id="acc_New"]
${editprofile_input_confirmpass}    xpath=//*[@id="acc_Con"]
${editprofile_btn_notsavepass}      xpath=//*[@id="btn_NotSavePassword"]
${editprofile_btn_savepass}         xpath=//*[@id="btn_SavePassword"]
${editprofile_btn_save}             xpath=//*[@id="btn_SaveProfile"]
${editprofile_changepass}           xpath=//*[@id="href_ChangePassword"]
${btn_edit_profile}                 xpath=/html/body/div/aside/div/nav/ul/li[2]/a

*** Keywords ***
Open web browser
    Open Browser  ${ums_url}     chrome
    Maximize Browser Window

Login with "${username}" "${password}"
    Input Text          ${ums_login_input_email}        ${username}
    Input Password      ${ums_login_input_password}     ${password}
    sleep                       1s
    Click Element       ${ums_btn_login}

Go to Edit profile
    sleep                       1s
    Click Element       ${btn_edit_profile}

Fill firstname "${firstname}"
    Input Text      ${editprofile_input_name}    ${firstname}

Fill lastname "${lastname}"
    Input Text      ${editprofile_input_lastname}    ${lastname}
    sleep                       1s

Fill currentpass "${currentpass}"
    Input Password      ${editprofile_input_curpass}    ${currentpass}

Fill newpass "${newpass}"
    Input Password      ${editprofile_input_newpass}    ${newpass}

Fill confirmpass "${confirmpass}"
    Input Password      ${editprofile_input_confirmpass}    ${confirmpass}
    sleep                       1s

Save profile
    sleep               1s
    Click Element       ${editprofile_btn_save}

Save profileandpassword
    sleep                1s
    Click Element       ${editprofile_btn_savepass}

Change password
    sleep                1s
    Click Element       ${editprofile_changepass}

The alert message must say "${message}"
    sleep                       1s
    Wait Until Page Contains    ${message}

*** Test Cases ***
# UMS-EditProfile-01 แก้ไขชื่อและนามสกุล
UMS-EditProfile-01-01
    [Documentation]     กรอกชื่อจริงและนามสกุลถูกต้อง
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Save profile
    THEN The alert message must say "User profile update successfully!"
    [Teardown]    Close Browser
UMS-EditProfile-01-02
    [Documentation]     กรอกชื่อจริงและนามสกุลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname "!Namchok"
    AND Fill lastname "!Singhachai"
    AND Save profile
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-01-03
    [Documentation]     กรอกชื่อจริงไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname "!Namchok"
    AND Save profile
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-01-04
    [Documentation]     กรอกนามสกุลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname "Namchok"
    AND Fill lastname "!Singhachai"
    AND Save profile
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-01-05
    [Documentation]     ไม่กรอกชื่อจริงและนามสกุล
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname " "
    AND Fill lastname " "
    AND Save profile
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-01-06
    [Documentation]     กรอกชื่อจริง แต่ไม่กรอกนามสกุล
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname "Namchok"
    AND Fill lastname " "
    AND Save profile
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-01-07
    [Documentation]     กรอกนามสกุล แต่ไม่กรอกชื่อจริง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Fill firstname " "
    AND Fill lastname "Singhachai"
    AND Save profile
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser

#--------------------------------------------------------------------------------------------------------------#
# UMS-EditProfile-01 แก้ไขชื่อและนามสกุล
UMS-EditProfile-02-01
    [Documentation]     กรอกชื่อจริง นามสกุล รหัสผ่านปัจจุบัน รหัสผ่านใหม่ และยืนยันรหัสผ่านถูกต้อง
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "User profile update successfully!"
    [Teardown]    Close Browser
UMS-EditProfile-02-02
    [Documentation]     กรอกชื่อจริง นามสกุล รหัสผ่านปัจจุบัน รหัสผ่านใหม่ และยืนยันรหัสผ่านไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "!Namchok"
    AND Fill lastname "!Singhachai"
    AND Fill currentpass "123qweQ"
    AND Fill newpass "123qweQ"
    AND Fill confirmpass "123qweQ"
    AND Save profileandpassword
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-03
    [Documentation]     กรอกชื่อจริงไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname " "
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-04
    [Documentation]     กรอกนามสกุลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "!Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-05
    [Documentation]     กรอกรหัสผ่านปัจจุบันไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The current password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-EditProfile-02-06
    [Documentation]     กรอกรหัสผ่านใหม่ไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The new password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-EditProfile-02-07
    [Documentation]     กรอกยืนยันรหัสผ่านไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ"
    AND Save profileandpassword
    THEN The alert message must say "The confirm password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-EditProfile-02-08
    [Documentation]     ไม่กรอกชื่อจริง นามสกุล รหัสผ่านปัจจุบัน รหัสผ่านใหม่ และยืนยันรหัสผ่าน
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname " "
    AND Fill lastname " "
    AND Fill currentpass " "
    AND Fill newpass " "
    AND Fill confirmpass " "
    AND Save profileandpassword
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-09
    [Documentation]     กรอกนามสกุล รหัสผ่านปัจจุบัน รหัสผ่านใหม่ และยืนยันรหัสผ่าน แต่ไม่กรอกชื่อจริง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname " "
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The First name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-10
    [Documentation]     กรอกชื่อจริง รหัสผ่านปัจจุบัน รหัสผ่านใหม่ และยืนยันรหัสผ่าน แต่ไม่กรอกนามสกุล 
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname " "
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The Last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-EditProfile-02-11
    [Documentation]     กรอกชื่อจริง นามสกุล รหัสผ่านใหม่ และยืนยันรหัสผ่าน แต่ไม่กรอกรหัสผ่านปัจจุบัน 
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass " "
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The current password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-EditProfile-02-12
    [Documentation]     กรอกชื่อจริง นามสกุล  รหัสผ่านปัจจุบัน และยืนยันรหัสผ่าน แต่ไม่กรอกรหัสผ่านใหม่
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass " "
    AND Fill confirmpass "123qweQ!"
    AND Save profileandpassword
    THEN The alert message must say "The new password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-EditProfile-02-13
    [Documentation]     กรอกชื่อจริง นามสกุล รหัสผ่านปัจจุบัน และรหัสผ่านใหม่ แต่ไม่กรอกยืนยันรหัสผ่าน
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to Edit profile
    AND Change password
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill currentpass "123qweQ!"
    AND Fill newpass "123qweQ!"
    AND Fill confirmpass " "
    AND Save profileandpassword
    THEN The alert message must say "The confirm password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser